﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Graph;
using Repository.Services;
using Repository.Xml;

namespace Repository
{
    public class LoaderService
    {
        private readonly DependencyResolverService<LoadedProject> _dependencyResolverService;
        private readonly InfoFactory _infoFactory;
        private readonly XmlService _xmlService;
        private readonly NamespaceFactory _namespaceFactory;
        private ResolveFileFunc? _lookupFunc;

        private bool _projectLoadFailed;

        public LoaderService(DependencyResolverService<LoadedProject> dependencyResolverService, InfoFactory infoFactory, XmlService xmlService, NamespaceFactory namespaceFactory)
        {
            _dependencyResolverService = dependencyResolverService;
            _infoFactory = infoFactory;
            _xmlService = xmlService;
            _namespaceFactory = namespaceFactory;
        }

        // Where 'targets' are toplevel projects. LoaderService resolves
        // dependent gir libraries automatically.
        public IEnumerable<LoadedProject> LoadOrdered(IEnumerable<string> targets, ResolveFileFunc lookupFunc)
        {
            _lookupFunc = lookupFunc;
            var loadedProjects = new List<LoadedProject>();

            foreach (var target in targets)
                LoadAndAddProjects(loadedProjects, target);

            if (_projectLoadFailed)
                Log.Warning($"Failed to load some projects. Please check the log for more information.");

            return _dependencyResolverService.ResolveOrdered(loadedProjects).Cast<LoadedProject>();
        }

        private void LoadAndAddProjects(ICollection<LoadedProject> loadedProjects, string target)
        {
            var info = new FileInfo(target);
            _ = LoadRecursive(loadedProjects, info);
        }

        private LoadedProject? LoadRecursive(ICollection<LoadedProject> loadedProjects, FileInfo target)
        {
            try
            {
                var repoinfo = LoadRepositoryInfo(target);
                var repositoryInfoData = _infoFactory.CreateFromNamespaceInfo(repoinfo.Namespace);

                if (TryLoadProject(loadedProjects, repositoryInfoData, out LoadedProject? project))
                    return project;

                var dependencies = LoadDependencies(loadedProjects, repoinfo.Includes);
                var nspace = _namespaceFactory.CreateFromNamespaceInfo(repoinfo.Namespace);
                
                project = new LoadedProject(nspace.ToCanonicalName(), nspace, dependencies);
                loadedProjects.Add(project);
                Log.Information($"Loaded '{nspace.ToCanonicalName()}' (provided by '{target.Name}')");

                return project;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load gir file '{target.Name}'.");
                Log.Exception(e);
                _projectLoadFailed = true;

                return null;
            }
        }

        private IEnumerable<LoadedProject> LoadDependencies(ICollection<LoadedProject> loadedProjects, IEnumerable<IncludeInfo> includes)
        {
            var dependencies = new List<LoadedProject>();
            foreach (var dependency in _infoFactory.CreateFromIncludes(includes))
            {
                FileInfo dependentGirFile = GetGirFileInfo(dependency);
                var dependentProject = LoadRecursive(loadedProjects, dependentGirFile);

                if (dependentProject is not null)
                    dependencies.Add(dependentProject);
            }

            return dependencies;
        }

        private bool TryLoadProject(IEnumerable<LoadedProject> loadedProjects, Info repo, [NotNullWhen(true)] out LoadedProject? loadedProject)
        {
            var foundProjects = loadedProjects.Where(x => NameMatches(x, repo)).ToArray();
            switch (foundProjects.Length)
            {
                case 0:
                    loadedProject = null;
                    return false;
                case 1:
                    loadedProject = foundProjects[0];
                    return true;
                default:
                    throw new Exception("Inconsistent data. Projects are loaded multiple times. Aborting");
            }
        }

        private bool NameMatches(LoadedProject loadedProject, Info repo)
            => loadedProject.Name == repo.ToCanonicalName();

        private FileInfo GetGirFileInfo(Info data)
        {
            if (_lookupFunc is null)
                throw new Exception("Lookup func is not initialized");

            return _lookupFunc(data.Name, data.Version);
        }

        private RepositoryInfo LoadRepositoryInfo(FileInfo target)
        {
            var repoInfo = _xmlService.Deserialize<RepositoryInfo>(target);

            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{target} does not define a namespace.");

            return repoInfo;
        }
    }
}

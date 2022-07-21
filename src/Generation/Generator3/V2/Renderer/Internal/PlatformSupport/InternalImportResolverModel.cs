﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Internal;

internal class InternalImportResolverModel
{
    public string InternalNamespaceName { get; }
    public string NamespaceName { get; }

    public string WindowsDll { get; }
    public string LinuxDll { get; }
    public string OsxDll { get; }

    public string WindowsType { get; }
    public string LinuxType { get; }
    public string OsxType { get; }

    public bool SupportsWindows { get; }
    public bool SupportsLinux { get; }
    public bool SupportsOSX { get; }

    public InternalImportResolverModel(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        InternalNamespaceName = GetInternalNamespaceName(linuxNamespace, macosNamespace, windowsNamespace);
        NamespaceName = GetNamespaceName(linuxNamespace, macosNamespace, windowsNamespace);

        if (linuxNamespace is not null && linuxNamespace.SharedLibrary is null)
            Log.Warning($"Shared library name for windows is not set for project {linuxNamespace.GetCanonicalName()}.");

        if (macosNamespace is not null && macosNamespace.SharedLibrary is null)
            Log.Warning($"Shared library name for windows is not set for project {macosNamespace.GetCanonicalName()}.");

        if (windowsNamespace is not null && windowsNamespace.SharedLibrary is null)
            Log.Warning($"Shared library name for windows is not set for project {windowsNamespace.GetCanonicalName()}.");

        LinuxType = linuxNamespace is null ? "string?" : "string";
        OsxType = macosNamespace is null ? "string?" : "string";
        WindowsType = windowsNamespace is null ? "string?" : "string";

        SupportsLinux = linuxNamespace is not null;
        SupportsOSX = macosNamespace is not null;
        SupportsWindows = windowsNamespace is not null;

        LinuxDll = GetDllName(linuxNamespace?.SharedLibrary);
        OsxDll = GetDllName(macosNamespace?.SharedLibrary);
        WindowsDll = GetDllName(windowsNamespace?.SharedLibrary);
    }

    private string GetInternalNamespaceName(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var names = new HashSet<string>();
        if (linuxNamespace is not null)
            names.Add(linuxNamespace.GetInternalName());
        if (macosNamespace is not null)
            names.Add(macosNamespace.GetInternalName());
        if (windowsNamespace is not null)
            names.Add(windowsNamespace.GetInternalName());

        return names.Count switch
        {
            0 => throw new Exception("Please provie at least one namespace"),
            1 => names.First(),
            _ => throw new Exception("Namespace internal names does not match")
        };
    }

    private string GetNamespaceName(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var names = new HashSet<string>();
        if (linuxNamespace is not null)
            names.Add(linuxNamespace.Name);
        if (macosNamespace is not null)
            names.Add(macosNamespace.Name);
        if (windowsNamespace is not null)
            names.Add(windowsNamespace.Name);

        return names.Count switch
        {
            0 => throw new Exception("Please provie at least one namespace"),
            1 => names.First(),
            _ => throw new Exception("Namespace names does not match")
        };
    }

    private string GetDllName(string? sharedLibrary)
    {
        if (sharedLibrary is null)
            return "null";

        var library = sharedLibrary
            .Split(',')
            .Select(Path.GetFileName)
            .OfType<string>()
            .FirstOrDefault(IsDllName) ?? sharedLibrary;

        return $"\"{library}\"";
    }

    private bool IsDllName(string name)
    {
        return name
            .Replace("-", "")
            .Replace("_", "")
            .Contains(NamespaceName, StringComparison.OrdinalIgnoreCase);
    }
}

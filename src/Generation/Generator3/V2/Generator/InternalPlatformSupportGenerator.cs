using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class InternalPlatformSupportGenerator
{
    public CodeUnit Generate(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var source = PlatformSupportImportResolverRenderer.Render(linuxNamespace, macosNamespace, windowsNamespace);
        var projectName = GetProjectName(linuxNamespace, macosNamespace, windowsNamespace);
        return new CodeUnit(projectName, "ImportResolver", source);
    }
    
    private string GetProjectName(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var names = new HashSet<string>();
        if (linuxNamespace is not null)
            names.Add(linuxNamespace.GetCanonicalName());
        if (macosNamespace is not null)
            names.Add(macosNamespace.GetCanonicalName());
        if (windowsNamespace is not null)
            names.Add(windowsNamespace.GetCanonicalName());

        return names.Count switch
        {
            0 => throw new Exception("Please provie at least one namespace"),
            1 => names.First(),
            _ => throw new Exception("Namespace internal names does not match")
        };
    }
}

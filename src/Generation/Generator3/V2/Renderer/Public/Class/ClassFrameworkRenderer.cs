﻿using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static class ClassFrameworkRenderer
{
    public static string Render(GirModel.Class cls)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace { cls.Namespace.GetPublicName() }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    {PlatformSupportAttributeRenderer.Render(cls as GirModel.PlatformDependent)}
    public partial class { cls.Name } {RenderInheritance(cls)}
    {{
        {RenderParentConstructors(cls)}
    }}
}}";
    }
    
    private static string RenderInheritance(GirModel.Class cls)
    {
        var parentClass = cls.Parent?.GetFullyQualified();
        var interfaces = cls.Implements.Select(x => x.GetFullyQualified());

        var elements = new List<string>(interfaces);

        if (parentClass is not null)
            elements.Insert(0, parentClass);

        return elements.Count == 0
            ? string.Empty
            : $": {string.Join(", ", elements)}";
    }
    
    private static string RenderParentConstructors(GirModel.Class cls)
    {
        if (cls.Parent is null)
            return string.Empty;

        var constructors = new List<string>()
        {
            $@"protected internal { cls.Name }(IntPtr ptr, bool ownedRef) : base(ptr, ownedRef) {{}}",
        };

        if (IsInitiallyUnowned(cls))
            constructors.Add($@"
// As initially unowned objects always start with a floating reference
// we can safely set the ""owned"" parameter to false.
protected internal { cls.Name }(params ConstructArgument[] constructArguments) : base(owned: false, constructArguments) {{}}");
        else if (InheritsInitiallyUnowned(cls))
            constructors.Add($"protected internal { cls.Name }(params ConstructArgument[] constructArguments) : base(constructArguments) {{}}");
        else
            constructors.Add($"protected internal { cls.Name }(bool owned, params ConstructArgument[] constructArguments) : base(owned, constructArguments) {{}}");

        return constructors.Join(Environment.NewLine);
    }

    private static bool IsInitiallyUnowned(GirModel.Class cls) => IsNamedInitiallyUnowned(cls.Name);

    private static bool InheritsInitiallyUnowned(GirModel.Class @class)
    {
        if (@class.Parent is null)
            return false;

        return IsNamedInitiallyUnowned(@class.Parent.Name) || InheritsInitiallyUnowned(@class.Parent);
    }

    private static bool IsNamedInitiallyUnowned(string name) => name == "InitiallyUnowned";
}

﻿using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static class InterfaceMethodsRenderer
{
    public static string Render(GirModel.Interface iface)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;

#nullable enable

namespace { iface.Namespace.GetPublicName() }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial interface { iface.Name }
    {{
        //TODO implement interface
    }}
}}";
    }
}

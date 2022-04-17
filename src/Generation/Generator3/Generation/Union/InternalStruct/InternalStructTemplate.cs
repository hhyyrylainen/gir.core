﻿using Generator3.Renderer;

namespace Generator3.Generation.Union
{
    public class InternalStructTemplate : Template<InternalStructModel>
    {
        public string Render(InternalStructModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    {model.PlatformDependent.RenderPlatformSupportAttributes()}
    [StructLayout(LayoutKind.Explicit)]
    public partial struct { model.Name }
    {{
        {model.Fields.Render()}
    }}
}}";
        }
    }
}

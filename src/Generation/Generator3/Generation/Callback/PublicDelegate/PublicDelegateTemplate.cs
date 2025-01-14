﻿using Generator3.Renderer.Public;

namespace Generator3.Generation.Callback
{
    public class PublicDelegateTemplate : Template<PublicDelegateModel>
    {
        public string Render(PublicDelegateModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public delegate {model.ReturnType.NullableTypeName} {model.Name}({model.Parameters.Render()});
}}";
        }
    }
}

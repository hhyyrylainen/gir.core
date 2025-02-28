﻿namespace Generator3.Generation.Interface
{
    public class PublicMethodsTemplate : Template<PublicMethodsModel>
    {
        public string Render(PublicMethodsModel model)
        {
            return $@"
using System;
using GObject;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial interface { model.Name }
    {{
        //TODO implement interface
    }}
}}";
        }
    }
}

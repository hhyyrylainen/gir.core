﻿namespace Generator3.Generation.Framework
{
    public class PublicModuleTypeRegistrationTemplate : Template<PublicModuleTypeRegistrationModel>
    {
        public string Render(PublicModuleTypeRegistrationModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

namespace {model.NamespaceName}
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    internal partial class Module
    {{
        static partial void RegisterTypes()
        {{
            {model.Classes.Render()}
            {model.Records.Render()}
            {model.Unions.Render()}
        }}
    }}
}}";
        }
    }
}

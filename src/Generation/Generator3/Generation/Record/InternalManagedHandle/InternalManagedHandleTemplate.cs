﻿using Generator3.Renderer;

namespace Generator3.Generation.Record
{
    public class InternalManagedHandleTemplate : Template<InternalManagedHandleModel>
    {
        public string Render(InternalManagedHandleModel model)
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
    public class { model.Name } : {model.BaseHandle}
    {{
        public static {model.BaseHandle} Create({model.InternalStruct}? data = null)
        {{
            var size = Marshal.SizeOf<{model.InternalStruct}>();
            IntPtr ptr = Marshal.AllocHGlobal(size);
                
            if (data.HasValue)
            {{
                Marshal.StructureToPtr(data.Value, ptr, false);
            }}
            else
            {{
                var str = new {model.InternalStruct}();
                Marshal.StructureToPtr(str, ptr, false);
            }}
                
            return new {model.Name}(ptr);
        }}
            
        private {model.Name}(IntPtr handle) : base(true) 
        {{
            SetHandle(handle); 
        }}
    
        protected override bool ReleaseHandle()
        {{
            Marshal.FreeHGlobal(handle);
            return true;
        }}
    }}
}}";
        }
    }
}

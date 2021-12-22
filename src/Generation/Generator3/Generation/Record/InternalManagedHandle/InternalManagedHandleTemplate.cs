﻿namespace Generator3.Generation.Record
{
    public class InternalManagedHandleTemplate : Template<InternalManagedHandleModel>
    {
        public string Render(InternalManagedHandleModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial class { model.Name }
    {{
        public class {model.HandleClassName} : {model.BaseHandle}
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
                    
                return new {model.HandleClassName}(ptr);
            }}
                
            private {model.HandleClassName}(IntPtr handle) : base(handle) {{ }}
        
            protected override bool ReleaseHandle()
            {{
                Marshal.FreeHGlobal(handle);
                return true;
            }}
        }}
    }}
}}";
        }
    }
}
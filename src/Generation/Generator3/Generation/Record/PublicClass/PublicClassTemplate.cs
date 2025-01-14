﻿using Generator3.Renderer;

namespace Generator3.Generation.Record
{
    public class PublicClassTemplate : Template<PublicClassModel>
    {
        public string Render(PublicClassModel model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {model.NamespaceName}
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    {model.PlatformDependent.RenderPlatformSupportAttributes()}
    public partial class {model.Name} : GLib.IHandle
    {{
        private readonly Internal.{model.InternalHandleName} _handle;

        public Internal.{model.InternalHandleName} Handle => !_handle.IsInvalid ? _handle : throw new Exception(""Invalid Handle"");
        IntPtr GLib.IHandle.Handle => _handle.DangerousGetHandle();

        // Override this to perform additional steps in the constructor
        partial void Initialize();
        
        public {model.Name}(Internal.{model.InternalHandleName} handle)
        {{
            _handle = handle;
            Initialize();
        }}

        //TODO: This is a workaround constructor as long as we are
        //not having https://github.com/gircore/gir.core/issues/397
        private {model.Name}(IntPtr ptr, bool ownsHandle) : this(ownsHandle
            ? new Internal.{model.InternalOwnedHandleName}(ptr)
            : new Internal.{model.InternalUnownedHandleName}(ptr)){{ }}

        // TODO: Default Constructor (allocate in managed memory and free on Dispose?)
        // We need to be able to create instances of records with full access to
        // fields, e.g. Gdk.Rectangle, Gtk.TreeIter, etc. 
        
        // TODO: Implement IDispose and free safe handle
    }}
}}";
        }
    }
}

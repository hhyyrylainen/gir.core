﻿using Generator3.Converter;

namespace Generator3.Generation.Record
{
    public class InternalHandleModel
    {
        public string HandleName => Record.GetInternalHandleName();
        public string NullHandleName => Record.GetInternalNullHandleName();
        public string UnownedHandleName => Record.GetInternalUnownedHandleName();
        public string InternalNamespaceName => Record.Namespace.GetInternalName();
        public GirModel.Record Record { get; }
        public GirModel.PlatformDependent? PlatformDependent => Record as GirModel.PlatformDependent;

        public InternalHandleModel(GirModel.Record record)
        {
            Record = record;
        }
    }
}

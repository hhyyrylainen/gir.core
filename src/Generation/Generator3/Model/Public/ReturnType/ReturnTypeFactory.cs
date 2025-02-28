﻿namespace Generator3.Model.Public
{
    public static class ReturnTypeFactory
    {
        public static ReturnType CreatePublicModel(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => type switch
            {
                GirModel.Pointer => new PointerReturnType(returnValue),
                GirModel.PrimitiveValueType => new PrimitiveValueReturnType(returnValue),
                GirModel.Class => new ClassReturnType(returnValue),
                GirModel.Interface => new InterfaceReturnType(returnValue),
                GirModel.Record => new RecordReturnType(returnValue),
                GirModel.Bitfield => new BitfieldReturnType(returnValue),
                GirModel.Enumeration => new EnumerationReturnType(returnValue),
                _ => new StandardReturnType(returnValue)
            },
            arrayType => arrayType switch
            {
                _ => new StandardReturnType(returnValue)
            }
        );
    }
}

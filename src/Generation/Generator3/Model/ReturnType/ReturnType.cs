﻿namespace Generator3.Model
{
    public abstract class ReturnType
    {
        protected readonly GirModel.ReturnType _returnValue;

        public abstract string NullableTypeName { get; }

        protected ReturnType(GirModel.ReturnType returnValue)
        {
            _returnValue = returnValue;
        }

        protected string GetDefaultNullable() => _returnValue.Nullable ? "?" : "";

        public static ReturnType CreateNative(GirModel.ReturnType returnValue) => returnValue switch
        {
            { Type: GirModel.String } => new Native.StringReturnType(returnValue),
            { Type: GirModel.Record } => new Native.RecordReturnType(returnValue),
            
            { Type: GirModel.ArrayType { Type : GirModel.Record}} => new Native.ArrayRecordReturnType(returnValue),

            _ => new Native.StandardReturnType(returnValue)
        };
    }
}

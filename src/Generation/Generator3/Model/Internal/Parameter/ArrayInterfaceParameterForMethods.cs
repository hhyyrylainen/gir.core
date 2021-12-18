﻿using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayInterfaceParameterForMethods : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        //Interfaces are always passed as a pointer. 
        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameConverter.Pointer
            : TypeNameConverter.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value + 1);

        protected internal ArrayInterfaceParameterForMethods(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Interface>();
        }
    }
}

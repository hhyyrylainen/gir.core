﻿using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class PrimitiveValueReturnType : ReturnType
    {
        private GirModel.PrimitiveValueType Type => (GirModel.PrimitiveValueType) Model.AnyType.AsT0;

        public override string NullableTypeName => Model.IsPointer
            ? TypeNameExtension.Pointer
            : Type.GetName();


        protected internal PrimitiveValueReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.PrimitiveValueType>();
        }
    }
}

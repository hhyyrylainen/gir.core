﻿using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PointerParameter : Parameter
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        public override string NullableTypeName => Model.AnyType.AsT0.GetName();

        protected internal PointerParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Pointer>();
        }
    }
}

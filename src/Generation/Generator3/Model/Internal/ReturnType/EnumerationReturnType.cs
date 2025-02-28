﻿using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class EnumerationReturnType : ReturnType
    {
        private GirModel.Enumeration Type => (GirModel.Enumeration) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        protected internal EnumerationReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Enumeration>();
        }
    }
}

﻿using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class InterfaceInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeNameExtension.Pointer;

        public InterfaceInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}

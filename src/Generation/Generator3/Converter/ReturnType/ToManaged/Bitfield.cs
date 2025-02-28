﻿using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Bitfield : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Bitfield>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}

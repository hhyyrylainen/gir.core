﻿namespace Generator3.Converter.Parameter.ToManaged;

internal class StringArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;
        if (parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            variableName = parameter.GetConvertedName();
            return $"var {variableName} = GLib.Internal.StringHelper.ToStringArrayUtf8({parameter.GetPublicName()});";
        }
        else
        {
            //We don't need any conversion for string[]
            variableName = parameter.GetPublicName();
            return null;
        }
    }
}

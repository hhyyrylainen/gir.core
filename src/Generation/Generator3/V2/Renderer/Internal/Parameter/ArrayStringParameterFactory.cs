using Generator3.Converter;
using GirModel;

namespace Generator3.Model.V2.Internal;

public static class ArrayStringParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
            { Transfer: Transfer.None, AnyType.AsT1.Length: null } => TypeNameExtension.Pointer,
            _ => parameter.AnyType.AsT1.GetName()
        };
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyType.AsT1.Length switch
        {
            null => string.Empty,
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l)
        };
    }
}

using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayStringParameterForMethodFactory
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
            { Transfer: GirModel.Transfer.None, AnyType.AsT1.Length: null } => TypeNameExtension.Pointer,
            _ => parameter.AnyType.AsT1.GetName()
        };
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyType.AsT1.Length switch
        {
            null => string.Empty,
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l + 1)
        };
    }
}

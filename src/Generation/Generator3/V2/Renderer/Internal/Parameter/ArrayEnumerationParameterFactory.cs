using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayEnumerationParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }

    private static string GetDirection(GirModel.Parameter parameter)
    {
        return parameter.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );
    }

    private static string GetNullableTypeName (GirModel.Parameter parameter)
    {
        var arrayType = parameter.AnyType.AsT1;
        var type = (GirModel.Enumeration) arrayType.AnyType.AsT0;

        return arrayType.Length is null
            ? TypeNameExtension.Pointer
            : type.GetFullyQualified() + "[]";
    }
}

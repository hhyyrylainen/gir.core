using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class StandardParameterFactory
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

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter.AnyType.Match(
            type => type.GetName() + (parameter.Nullable ? "?" : ""),
            arrayType => arrayType.GetName() //TODO: Consider if StandardParameter should support arrays?
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

using Generator3.Converter;

namespace Generator3.Model.V2.Public;

public static class ArrayClassParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetPublicName()
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var cls = (GirModel.Class) parameter.AnyType.AsT1.AnyType.AsT0;
        return cls.GetFullyQualified() + "[]";
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

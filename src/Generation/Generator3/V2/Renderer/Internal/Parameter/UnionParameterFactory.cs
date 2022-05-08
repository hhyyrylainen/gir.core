using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class UnionParameterFactory
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
        return parameter.IsPointer switch
        {
            true => TypeNameExtension.Pointer,
            false => parameter.AnyType.AsT0.GetName()
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

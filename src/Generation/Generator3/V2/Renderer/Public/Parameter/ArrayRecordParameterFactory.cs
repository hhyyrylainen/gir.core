using Generator3.Converter;

namespace Generator3.Model.V2.Public;

public static class ArrayRecordParameterFactory
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
        var arrayType = parameter.AnyType.AsT1;
        
        return arrayType.Length is null
            ? TypeNameExtension.PointerArray
            : ((GirModel.Record) arrayType.AnyType.AsT0).GetFullyQualified() + "[]";
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

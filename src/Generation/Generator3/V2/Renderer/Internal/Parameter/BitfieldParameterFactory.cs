using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class BitfieldParameterFactory
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

    private static string GetNullableTypeName(GirModel.Parameter parameter) => parameter.IsPointer switch
    {
        true => TypeNameExtension.Pointer,
        //Internal does not define any bitfields. They are part of the Public API to avoid converting between them.
        false => ((GirModel.Bitfield) parameter.AnyType.AsT0).GetFullyQualified()
    };

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

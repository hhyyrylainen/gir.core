using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class PointerRecordParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: TypeNameExtension.Pointer,
            Name: parameter.GetInternalName()
        );
    }
}

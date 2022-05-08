using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class CallbackFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: field.Name,
            Attribute: null,
            NullableTypeName: field.AnyTypeOrCallback.AsT1.GetInternalName()
        );
    }
}

using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class StringFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: field.Name,
            Attribute: MarshalAs.UnmanagedLpString(),
            NullableTypeName: field.AnyTypeOrCallback.AsT0.AsT0.GetName()
        );
    }
}

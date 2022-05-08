using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class StandardFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: field.Name,
            Attribute: null,
            NullableTypeName: GetNullableTypeName(field)
        );
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        return field.IsPointer
            ? TypeNameExtension.Pointer
            : field.AnyTypeOrCallback.AsT0.AsT0.GetName();
    }
}

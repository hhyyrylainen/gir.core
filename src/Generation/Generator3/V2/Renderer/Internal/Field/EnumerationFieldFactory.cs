using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class EnumerationFieldFactory
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
        var type = (GirModel.Enumeration) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? TypeNameExtension.Pointer
            : type.GetFullyQualified();
    }
}

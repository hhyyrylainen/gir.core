using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class RecordFieldFactory
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
        var type = (GirModel.Record) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? TypeNameExtension.Pointer
            : type.GetFullyQualifiedInternalStructName();
    }
}

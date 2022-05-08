using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayRecordFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: field.Name,
            Attribute: GetAttribute(field),
            NullableTypeName: GetNullableTypeName(field)
        );
    }

    private static string? GetAttribute(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        return arrayType.FixedSize is not null
            ? MarshalAs.UnmanagedByValArray(sizeConst: arrayType.FixedSize.Value)
            : null;
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        var type = (GirModel.Record) arrayType.AnyType.AsT0;
        return type.GetFullyQualifiedInternalStructName() + "[]";
    }
}

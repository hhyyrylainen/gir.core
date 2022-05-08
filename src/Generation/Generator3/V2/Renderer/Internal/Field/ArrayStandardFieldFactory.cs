namespace Generator3.Model.V2.Internal;

public static class ArrayStandardFieldFactory
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
        return arrayType.GetName();
    }
}

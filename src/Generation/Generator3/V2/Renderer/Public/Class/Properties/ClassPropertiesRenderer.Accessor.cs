using System.Text;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassPropertiesRenderer
{
    private static string RenderAccessor(GirModel.Class cls, GirModel.Property property)
    {
        if (!property.Readable && !property.Writeable)
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public {property.GetNullableTypeName()} {property.GetPublicName()}");
        builder.AppendLine("{");

        if (property.Readable)
            builder.AppendLine($"    get => {GetGetter(cls, property)};");

        if (property.Writeable && !property.ConstructOnly)
            builder.AppendLine($"    set => {GetSetter(cls, property)};");

        builder.AppendLine("}");

        return builder.ToString();
    }

    private static string GetGetter(GirModel.Class cls, GirModel.Property property)
    {
        return property.SupportsAccessorGetMethod(out var getter)
            ? $"Internal.{cls.Name}.{getter.GetInternalName()}(Handle)"
            : $"GetProperty({property.GetDescriptorName()})";
    }

    private static string GetSetter(GirModel.Class cls, GirModel.Property property)
    {
        return property.SupportsAccessorSetMethod(out var setter)
            ? $"Internal.{cls.Name}.{setter.GetInternalName()}(Handle, value)"
            : $"SetProperty({property.GetDescriptorName()}, value)";
    }
}

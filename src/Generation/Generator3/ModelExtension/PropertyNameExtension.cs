using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Generator3.Model;
using Generator3.Model.V2.Internal;

namespace Generator3.Converter
{
    public static class PropertyNameExtension
    {
        public static string GetPublicName(this GirModel.Property property)
        {
            return property.Name.ToPascalCase();
        }

        public static string GetDescriptorName(this GirModel.Property property)
        {
            return GetPublicName(property) + "PropertyDefinition";
        }
        
        public static string GetNullableTypeName(this GirModel.Property property)
        {
            return property.AnyType.Match(
                type => type switch
                {
                    GirModel.ComplexType c => c.GetFullyQualified() + GetDefaultNullable(property),
                    _ => type.GetName() + GetDefaultNullable(property)
                },
                arrayType => arrayType.GetName()
            );
        }
        
        // Properties need special nullable handling as each C value is implicitly nullable.
        // The C# Marshaller returns default values for primitive value types. But there
        // can be null values for strings and objects, even if the nullability attribute
        // is not present in the gir file
        private static string GetDefaultNullable(GirModel.Property property)
        {
            if (!property.AnyType.TryPickT0(out var type, out _))
                return string.Empty;

            switch (type)
            {
                case GirModel.String:
                case GirModel.Class:
                    return "?";
                default:
                    return string.Empty;
            }
        }
        
        public static bool SupportsAccessorGetMethod(this GirModel.Property property, [NotNullWhen(true)] out GirModel.Method? getter)
        {
            if (!property.Readable || property.Getter is null)
            {
                getter = null;
                return false;
            }

            if (property.Getter.Parameters.Any())
            {
                //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
                getter = null;
                return false;
            }

            getter = property.Getter;
            var getterType = property.Getter.ReturnType
                .Map(RenderableReturnTypeFactory.Create)
                .NullableTypeName;

            return GetNullableTypeName(property) == getterType;
        }
        
        public static bool SupportsAccessorSetMethod(this GirModel.Property property, [NotNullWhen(true)] out GirModel.Method? setter)
        {
            if (!property.Writeable || property.Setter is null)
            {
                setter = null;
                return false;
            }

            if (property.Setter.Parameters.Count() > 1)
            {
                //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
                setter = null;
                return false;
            }

            setter = property.Setter;
            var setterType = property.Setter.Parameters
                .First()
                .Map(RenderableParameterFactory.Create)
                .NullableTypeName;

            return GetNullableTypeName(property) == setterType;
        }
    }
}

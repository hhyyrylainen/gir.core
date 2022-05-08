using System;
using System.Collections.Generic;
using System.Text;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassPropertiesRenderer
{
    private static string RenderDescriptor(GirModel.Class cls, GirModel.Property property)
    {
        if (!property.Readable && !property.Writeable)
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public static readonly Property<{property.GetNullableTypeName()}> {property.GetDescriptorName()} = Property<{property.GetNullableTypeName()}>.Register<{cls.Name}>(");

        builder.AppendLine(string.Join($",{Environment.NewLine}    ", GetArguments(property)));

        builder.AppendLine(");");
        return builder.ToString();
    }

    private static IEnumerable<string> GetArguments(GirModel.Property property)
    {
        var arguments = new List<string>()
        {
            $"nativeName: \"{property.Name}\"",
            $"managedName: nameof({property.GetPublicName()})"
        };

        if (property.Readable)
            arguments.Add($"get: o => o.{property.GetPublicName()}");

        if (property.Writeable && !property.ConstructOnly)
            arguments.Add($"set: (o, v) => o.{property.GetPublicName()} = v");

        return arguments;
    }

}

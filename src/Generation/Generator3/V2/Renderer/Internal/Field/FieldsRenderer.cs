using System.Collections.Generic;
using System.Linq;
using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Internal;

public static class FieldsRenderer
{
    public static string Render(IEnumerable<GirModel.Field> fields)
    {
        return fields
            .Select(RenderableFieldFactory.Create)
            .Select(Render)
            .Join(", ");
    }

    private static string Render(RenderableField field)
    {
        return @$"{field.Attribute} public {field.NullableTypeName} {field.Name};";
    }
}

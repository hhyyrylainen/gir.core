using System.Collections.Generic;
using System.Linq;
using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Internal;

public static class ParametersRenderer
{
    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
            .Select(RenderableParameterFactory.Create)
            .Select(Render)
            .Join(", ");
    }
    
    private static string Render(RenderableParameter parameter)
        => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
}

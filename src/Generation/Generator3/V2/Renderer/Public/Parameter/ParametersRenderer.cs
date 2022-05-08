using System.Collections.Generic;
using System.Linq;
using Generator3.Model.V2.Public;

namespace Generator3.Renderer.V2.Public;

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
        => $@"{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
}

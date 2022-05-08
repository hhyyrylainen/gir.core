using System.Collections.Generic;
using System.Text;
using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    private static string RenderAsSignalParammeters(IEnumerable<GirModel.Parameter> parameters)
    {
        var sb = new StringBuilder();
        var index = 1; //Argument 0 is reserved

        foreach (var parameter in parameters)
        {
            sb.AppendLine(RenderAsSignalParammeter(parameter, index));
            index++;
        }

        return sb.ToString();
    }

    private static string RenderAsSignalParammeter(GirModel.Parameter parameter, int index)
    {
        var p = RenderableParameterFactory.Create(parameter);
            
        return $@"public {p.NullableTypeName} {parameter.Name.ToPascalCase()} => Args[{index}].Extract<{p.NullableTypeName}>();";
    }
}

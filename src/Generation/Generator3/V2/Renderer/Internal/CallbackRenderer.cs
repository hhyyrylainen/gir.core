using System.Collections.Generic;
using System.Linq;
using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Internal;

public static class CallbackRenderer
{
    public static string Render(GirModel.Callback callback)
    {
        try
        {
            return $"public delegate {ReturnTypeRenderer.Render(callback.ReturnType)} {callback.Name}({RenderParameters(callback.Parameters)});";
        }
        catch (System.Exception ex)
        {
            Log.Warning($"Could not render internal callback: {callback.Name}: {ex.Message}");
            return string.Empty;
        }
    }

    private static string RenderParameters(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
                .Select(RenderableCallbackParameterFactory.Create)
                .Select(parameter => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}")
                .Join(", ");
    }
}

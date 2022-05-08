using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.V2.Internal;

public static class MethodsRenderer
{
    public static string Render(IEnumerable<GirModel.Method> methods)
    {
        return methods
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Method? method)
    {
        if (method is null)
            return string.Empty;

        var separator = method.Parameters.Any() ? ", " : string.Empty;

        return $@"
/// <summary>
/// Calls native method {method.CIdentifier}.
/// </summary>
{DocCommentsRenderer.Render(method.Parameters)}
{DocCommentsRenderer.Render(method.ReturnType)}
[DllImport(ImportResolver.Library, EntryPoint = ""{ method.CIdentifier }"")]
public static extern { ReturnTypeRenderer.Render(method.ReturnType) } { method.Name }({InstanceParametersRenderer.Render(method.InstanceParameter)}{separator}{ ParametersRenderer.Render(method.Parameters) });";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Internal;

public static class ConstructorsRenderer
{
    public static string Render(IEnumerable<GirModel.Constructor> constructors)
    {
        return constructors
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Constructor? constructor)
    {
        if (constructor is null)
            return string.Empty;

        return @$"
/// <summary>
/// Calls native constructor {constructor.CIdentifier}.
/// </summary>
{DocCommentsRenderer.Render(constructor.Parameters)}
{DocCommentsRenderer.Render(constructor.ReturnType)}
[DllImport(ImportResolver.Library, EntryPoint = ""{ constructor.CIdentifier }"")]
public static extern { ReturnTypeRenderer.Render(constructor.ReturnType) } { constructor.GetInternalName() }({ ParametersRenderer.Render(constructor.Parameters) });";
    }
}

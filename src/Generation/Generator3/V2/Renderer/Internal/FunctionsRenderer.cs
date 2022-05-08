using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Internal;

public static class FunctionsRenderer
{
    public static string Render(IEnumerable<GirModel.Function> functions)
    {
        return functions
            .Select(Render)
            .Join(Environment.NewLine);
    }
    
    public static string Render(GirModel.Function? function)
    {
        if (function is null)
            return string.Empty;

        try
        {
            return @$"
/// <summary>
/// Calls native function {function.CIdentifier}.
/// </summary>
{DocCommentsRenderer.Render(function.Parameters)}
{DocCommentsRenderer.Render(function.ReturnType)}
{PlatformSupportAttributeRenderer.Render(function as GirModel.PlatformDependent)}
[DllImport(ImportResolver.Library, EntryPoint = ""{ function.CIdentifier }"")]
public static extern { ReturnTypeRenderer.Render(function.ReturnType) } { function.GetInternalName() }({ ParametersRenderer.Render(function.Parameters)});";
        }
        catch (Exception ex)
        {
            Log.Warning($"Could not render internal function \"{function.CIdentifier}\": {ex.Message}");
            return string.Empty;
        }
    }
}

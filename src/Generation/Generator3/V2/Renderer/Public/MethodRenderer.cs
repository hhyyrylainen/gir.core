using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator3.Converter;
using Generator3.Model.Public;
using Generator3.Renderer.V2.Public;

namespace Generator3.V2.Renderer.Public;

public static class MethodRenderer
{
    public static string Render(GirModel.Class cls, GirModel.Method method)
    {
        try
        {
            //We do not need to create a method for free methods. Freeing is handled by
            //the framework via a IDisposable implementation.
            return method.IsFree()
                ? string.Empty
                : RenderInternal(cls, method);
        }
        catch (Exception e)
        {
            var message = $"Did not generate method '{cls.Name}.{method.GetPublicName()}': {e.Message}";

            if (e is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static string RenderInternal(GirModel.Class cls, GirModel.Method method)
    {
        var publicReturnType = method.ReturnType.CreatePublicModel();

        return @$"
public {publicReturnType.NullableTypeName} {method.GetPublicName()}({ParametersRenderer.Render(method.Parameters)})
{{
    {ParametersToNativeConverter.RenderToNative(method.Parameters, out var parameterNames)}
    {RenderCallStatement(cls, method, parameterNames, out var resultVariableName)}
    {RenderReturnStatement(method, resultVariableName)}
}}";
    }
        
    private static string RenderCallStatement(GirModel.Class cls, GirModel.Method method, IEnumerable<string> parameterNames, out string resultVariableName)
    {
        resultVariableName = "result";
        var call = new StringBuilder();

        if (!method.ReturnType.AnyType.Is<GirModel.Void>())
            call.Append($"var {resultVariableName} = ");

        call.Append($"Internal.{cls.Name}.{method.GetInternalName()}(");
        call.Append("this.Handle" + (parameterNames.Any() ? ", " : string.Empty));
        call.Append(string.Join(", ", parameterNames));
        call.Append(");\n");

        return call.ToString();
    }
        
    private static string RenderReturnStatement(GirModel.Method method, string returnVariable)
    {
        return method.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {method.ReturnType.ToManaged(returnVariable)};";
    }
}

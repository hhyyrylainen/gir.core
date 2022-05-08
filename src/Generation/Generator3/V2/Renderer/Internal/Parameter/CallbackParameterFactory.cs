using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class CallbackParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }
    
    //Internal callbacks are not nullable
    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Callback) parameter.AnyType.AsT0;
        return type.Namespace.GetInternalName() + "." + type.GetName();
    }
}

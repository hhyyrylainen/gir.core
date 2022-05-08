using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Internal;

public static class InstanceParametersRenderer
{
    public static string Render(GirModel.InstanceParameter parameter)
    {
        return parameter
            .Map(RenderableInstanceParameterFactory.Create)
            .Map(Render);
    }
    
    private static string Render(RenderableInstanceParameter parameter)
        => $@"{parameter.NullableTypeName} {parameter.Name}";
}

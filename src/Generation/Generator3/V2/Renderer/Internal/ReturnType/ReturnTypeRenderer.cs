using Generator3.Model.V2.Internal;

namespace Generator3.Renderer.V2.Internal;

public static class ReturnTypeRenderer
{
    public static string Render(GirModel.ReturnType returnType)
    {
        return returnType
            .Map(RenderableReturnTypeFactory.Create)
            .NullableTypeName;
    }
}

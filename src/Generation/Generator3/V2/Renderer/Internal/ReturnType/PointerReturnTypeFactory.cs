using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class PointerReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(TypeNameExtension.Pointer);
    }
}

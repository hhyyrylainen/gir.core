using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class PrimitiveValueReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? TypeNameExtension.Pointer
            : returnType.AnyType.AsT0.GetName();

        return new RenderableReturnType(nullableTypeName);
    }
}

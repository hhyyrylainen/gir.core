namespace Generator3.Model.V2.Internal;

public static class ArrayPrimitiveValueReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(returnType.AnyType.AsT1.GetName());
    }
}

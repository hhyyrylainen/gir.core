using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class UnionReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Union) returnType.AnyType.AsT0;
        
        var nullableTypeName = returnType.IsPointer
            ? TypeNameExtension.Pointer
            : type.GetFullyQualifiedInternalStructName();

        return new RenderableReturnType(nullableTypeName);
    }
}

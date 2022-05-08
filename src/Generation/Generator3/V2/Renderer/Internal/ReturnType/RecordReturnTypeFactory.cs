using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class RecordReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;
        
        var nullableTypeName = !returnType.IsPointer
            ? type.GetFullyQualifiedInternalStructName()
            : returnType.Transfer == GirModel.Transfer.None
                ? type.GetFullyQualifiedInternalUnownedHandle()
                : type.GetFullyQualifiedInternalOwnedHandle();

        return new RenderableReturnType(nullableTypeName);
    }
}

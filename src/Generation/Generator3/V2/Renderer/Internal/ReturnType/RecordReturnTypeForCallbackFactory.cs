using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class RecordReturnTypeForCallbackFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;
        
        var nullableTypeName = !returnType.IsPointer
            ? type.GetFullyQualifiedInternalStructName()
            : type.GetFullyQualifiedInternalHandle();

        return new RenderableReturnType(nullableTypeName);
    }
}

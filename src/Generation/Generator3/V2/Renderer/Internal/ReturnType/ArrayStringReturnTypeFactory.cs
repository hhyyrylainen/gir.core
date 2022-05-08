using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayStringReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var arrayType = returnType.AnyType.AsT1;
        var isMarshalAble = returnType.Transfer != GirModel.Transfer.None || arrayType.Length != null;
        var nullableTypeName = isMarshalAble
            ? arrayType.GetName()
            : TypeNameExtension.Pointer;

        return new RenderableReturnType(nullableTypeName);
    }
}

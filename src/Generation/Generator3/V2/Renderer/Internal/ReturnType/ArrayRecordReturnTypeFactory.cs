using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayRecordReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        //Internal arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        return new RenderableReturnType(TypeNameExtension.PointerArray);
    }
}

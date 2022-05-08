using Generator3.Converter;
using Generator3.Renderer.V2;

namespace Generator3.Model.V2.Internal;

public static class PlatformStringReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType switch
        {
            // Return values which return a string without transferring ownership to us can not be marshalled automatically
            // as the marshaller want's to free the unmanaged memory which is not allowed if the ownership is not transferred
            { Transfer: GirModel.Transfer.None } => TypeNameExtension.Pointer,
            _ => returnType.AnyType.AsT0.GetName() + returnType.RenderNullable()
        };

        return new RenderableReturnType(nullableTypeName);
    }
}

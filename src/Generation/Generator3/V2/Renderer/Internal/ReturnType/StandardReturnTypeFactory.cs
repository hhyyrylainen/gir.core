using Generator3.Converter;
using Generator3.Renderer.V2;

namespace Generator3.Model.V2.Internal;
//TODO: Consider Removing all "Standard" model classes as it is not clear in which cases they are used explicitly and replace them with concrete implementations

public static class StandardReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.AnyType.Match(
            type => type.GetName() + returnType.RenderNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if arrayType should be supported by this class
        );

        return new RenderableReturnType(nullableTypeName);
    }
}

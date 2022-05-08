using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class EnumerationReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Enumeration) returnType.AnyType.AsT0;
        return new RenderableReturnType(type.GetFullyQualified());
    }
}

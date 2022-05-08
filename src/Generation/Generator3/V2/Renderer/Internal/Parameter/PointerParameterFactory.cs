using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class PointerParameterFactory
{
    //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: parameter.AnyType.AsT0.GetName(),
            Name: parameter.GetInternalName()
        );
    }
}

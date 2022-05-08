using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class UnsignedPointerParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }
    
    //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
    private static string GetNullableTypeName(GirModel.Parameter parameter) 
        => parameter.AnyType.AsT0.GetName();
}

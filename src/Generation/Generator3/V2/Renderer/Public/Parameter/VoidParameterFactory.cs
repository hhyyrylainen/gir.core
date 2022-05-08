using Generator3.Converter;

namespace Generator3.Model.V2.Public;

public class VoidParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Direction: string.Empty,
            NullableTypeName: TypeNameExtension.Pointer,
            Name: parameter.GetPublicName()
        );
    }
}

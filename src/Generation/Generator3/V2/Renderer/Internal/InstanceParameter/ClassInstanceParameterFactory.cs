using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ClassInstanceParameterFactory
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter)
    {
        return new RenderableInstanceParameter(
            Name: instanceParameter.GetInternalName(),
            NullableTypeName: TypeNameExtension.Pointer
        );
    }
}

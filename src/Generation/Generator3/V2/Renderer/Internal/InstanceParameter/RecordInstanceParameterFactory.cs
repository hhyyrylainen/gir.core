using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class RecordInstanceParameterFactory
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter)
    {
        var type = (GirModel.Record) instanceParameter.Type;
        
        return new RenderableInstanceParameter(
            Name: instanceParameter.GetInternalName(),
            NullableTypeName: type.GetFullyQualifiedInternalHandle()
        );
    }
}

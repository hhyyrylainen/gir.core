using System;

namespace Generator3.Model.V2.Internal;

public static class RenderableInstanceParameterFactory
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter) => instanceParameter.Type switch
    {
        GirModel.Pointer => PointerInstanceParameterFactory.Create(instanceParameter),
        GirModel.Class => ClassInstanceParameterFactory.Create(instanceParameter),
        GirModel.Interface => InterfaceInstanceParameterFactory.Create(instanceParameter),
        GirModel.Record => RecordInstanceParameterFactory.Create(instanceParameter),
        GirModel.Union => UnionInstanceParameterFactory.Create(instanceParameter),

        _ => throw new Exception($"Instance parameter \"{instanceParameter.Name}\" of type {instanceParameter.Type} can not be converted into a model")
    };
}

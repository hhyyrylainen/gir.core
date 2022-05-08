using System;

namespace Generator3.Model.V2.Public;

public static class RenderableParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter) => parameter.AnyType.Match(
        type => type switch
        {
            GirModel.Class => ClassParameterFactory.Create(parameter),
            GirModel.Interface => InterfaceParameterFactory.Create(parameter),
            GirModel.Bitfield => BitfieldParameterFactory.Create(parameter),
            GirModel.Enumeration => EnumerationParameterFactory.Create(parameter),
            GirModel.Union => UnionParameterFactory.Create(parameter),
            GirModel.Record => RecordParameterFactory.Create(parameter),
            GirModel.Void => VoidParameterFactory.Create(parameter),
            _ => StandardParameterFactory.Create(parameter) //TODO: Remove Standard Parameter
        },
        arraytype => arraytype.AnyType.Match(
            type => type switch
            {
                GirModel.Record => ArrayRecordParameterFactory.Create(parameter),
                GirModel.Class => ArrayClassParameterFactory.Create(parameter),
                _ => StandardParameterFactory.Create(parameter)
            },
            _ => throw new NotSupportedException("Arrays of arrays not yet supported")
        )
    );
}

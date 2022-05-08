using System;

namespace Generator3.Model.V2.Internal;

public static class MethodParameterFactory
{
    public static RenderableParameter Create(this GirModel.Parameter parameter) => parameter.AnyType.Match(
        type => type switch
        {
            GirModel.String => StringParameterFactory.Create(parameter),
            GirModel.Pointer => PointerParameterFactory.Create(parameter),
            GirModel.UnsignedPointer => UnsignedPointerParameterFactory.Create(parameter),
            GirModel.Class => ClassParameterFactory.Create(parameter),
            GirModel.Interface => InterfaceParameterFactory.Create(parameter),
            GirModel.Union => UnionParameterFactory.Create(parameter),
            GirModel.Record => RecordParameterFactory.Create(parameter),
            GirModel.PrimitiveValueType => PrimitiveValueTypeParameterFactory.Create(parameter),
            GirModel.Callback => CallbackParameterFactory.Create(parameter),
            GirModel.Enumeration => EnumerationParameterFactory.Create(parameter),
            GirModel.Bitfield => BitfieldParameterFactory.Create(parameter),
            GirModel.Void => VoidParameterFactory.Create(parameter),

            _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
        },
        arrayType => arrayType.AnyType.Match(
            type => type switch
            {
                GirModel.Class => ArrayClassParameterForMethodsFactory.Create(parameter),
                GirModel.Interface => ArrayInterfaceParameterForMethodsFactory.Create(parameter),
                GirModel.Record when arrayType.IsPointer => ArrayPointerRecordParameterForMethodFactory.Create(parameter),
                GirModel.Record => ArrayRecordParameterForMethodFactory.Create(parameter),
                GirModel.String => ArrayStringParameterForMethodFactory.Create(parameter),
                GirModel.Enumeration => ArrayEnumerationParameterFactory.Create(parameter),
                _ => StandardParameterFactory.Create(parameter)
            },
            _ => throw new NotSupportedException("Arrays of arrays not yet supported")
        )
    );
}

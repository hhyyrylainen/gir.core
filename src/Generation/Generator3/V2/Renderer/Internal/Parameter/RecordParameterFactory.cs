using System;
using Generator3.Converter;
using GirModel;

namespace Generator3.Model.V2.Internal;

public static class RecordParameterFactory
{
    public static RenderableParameter Create(Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }

    //Native records are represented as SafeHandles and are not nullable
    private static string GetNullableTypeName(Parameter parameter)
    {
        var type = (Record) parameter.AnyType.AsT0;
        return parameter switch
        {
            { Direction: Direction.In } => type.GetFullyQualifiedInternalHandle(),
            { CallerAllocates: true } => type.GetFullyQualifiedInternalHandle(),
            { CallerAllocates: false, Direction: Direction.InOut, Transfer: Transfer.Full } => type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: Direction.InOut, Transfer: Transfer.Container } => type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: Direction.InOut, Transfer: Transfer.None } => type.GetFullyQualifiedInternalUnownedHandle(),
            { CallerAllocates: false, Direction: Direction.Out, Transfer: Transfer.Full } => type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: Direction.Out, Transfer: Transfer.Container } => type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: Direction.Out, Transfer: Transfer.None } => type.GetFullyQualifiedInternalUnownedHandle(),
            _ => throw new Exception($"Can't detect parameter type: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.In,
        @inout: ParameterDirection.In
    );
}

using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class ArrayRecordParameterForMethodFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var arrayType = parameter.AnyType.AsT1;
        
        return arrayType.Length is null
            ? TypeNameExtension.PointerArray
            : ((GirModel.Record) arrayType.AnyType.AsT0).GetFullyQualifiedInternalStructName() + "[]";
    }
    
    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyType.AsT1.Length switch
        {
            { } length => MarshalAs.UnmanagedLpArray(sizeParamIndex: length + 1),
            _ => string.Empty,
        };
    }
}

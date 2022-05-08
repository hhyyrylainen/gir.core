using Generator3.Converter;

namespace Generator3.Model.V2.Internal;

public static class StringParameterFactory
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: parameter.GetInternalName()
        );
    }
    
    private static string GetAttribute(GirModel.Parameter parameter) => parameter.AnyType.AsT0 switch
    {
        // Marshal as a UTF-8 encoded string
        GirModel.Utf8String => MarshalAs.UnmanagedLpUtf8String(),

        // Marshal as a null-terminated array of ANSI characters
        // TODO: This is likely incorrect:
        //  - GObject introspection specifies that Windows should use
        //    UTF-8 and Unix should use ANSI. Does using ANSI for
        //    everything cause problems here?
        GirModel.PlatformString => MarshalAs.UnmanagedLpString(),

        _ => ""
    };
    
    private static string GetNullableTypeName(GirModel.Parameter parameter) 
        => parameter.AnyType.AsT0.GetName() + (parameter.Nullable ? "?" : "");

    private static string GetDirection(GirModel.Parameter parameter) => parameter.GetDirection(
        @in: ParameterDirection.In,
        @out: ParameterDirection.Out,
        @outCallerAllocates: ParameterDirection.Ref,
        @inout: ParameterDirection.Ref
    );
}

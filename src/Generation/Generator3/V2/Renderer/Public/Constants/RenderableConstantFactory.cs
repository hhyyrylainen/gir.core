using Generator3.Converter;
using Generator3.Model.V2.Public;

namespace Generator3.Renderer.V2.Public;

public static class RenderableConstantFactory
{
    public static RenderableConstant Create(GirModel.Constant constant)
    {
        return new RenderableConstant(
            Type: constant.Type.GetName(),
            Name: constant.GetPublicName(),
            Value: GetValue(constant)
        );
    }
    
    private static string GetValue(GirModel.Constant constant)
    {
        return constant.Type switch
        {
            GirModel.Bitfield { Name: { } name } => $"({name}) {constant.Value}",
            GirModel.String => "\"" + constant.Value + "\"",
            _ => constant.Value
        };
    }
}

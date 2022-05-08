using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    private static string RenderEvent(GirModel.Class cls, GirModel.Signal signal)
    {
        return $@"
public event SignalHandler<{GetGenericArgs(cls, signal)}> {signal.GetPublicName()}
{{
    add => {signal.GetDescriptorName()}.Connect(this, value);
    remove => {signal.GetDescriptorName()}.Disconnect(this, value);
}}";
    }
}

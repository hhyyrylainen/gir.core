using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    private static string RenderDescriptor(GirModel.Class cls, GirModel.Signal signal)
    {
        return @$"
/// <summary>
/// Signal Descriptor for {signal.GetPublicName()}.
/// </summary>
public static readonly Signal<{GetGenericArgs(cls, signal)}> {signal.GetDescriptorName()} = Signal<{GetGenericArgs(cls, signal)}>.Wrap(""{signal.Name}"");";
    }
}

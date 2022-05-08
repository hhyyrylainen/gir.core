using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    private static string RenderArgsIndexer(GirModel.Class cls, GirModel.Signal signal)
    {
        return !signal.Parameters.Any()
            ? string.Empty
            : @$"
/// <summary>
/// Indexer to connect {signal.GetDescriptorName()} with a SignalHandler&lt;{GetGenericArgs(cls, signal)}&gt;
/// </summary>
public SignalHandler<{GetGenericArgs(cls, signal)}> this[Signal<{GetGenericArgs(cls, signal)}> signal]
{{
    set => signal.Connect(this, value, true);
}}";
    }
}

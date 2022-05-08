using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    private static string RenderArgs(GirModel.Signal signal)
    {
        return !signal.Parameters.Any()
            ? string.Empty
            : @$"
/// <summary>
/// Signal (Event) Arguments for {signal.GetPublicName()}
/// </summary>
public sealed class {signal.GetArgsClassName()} : SignalArgs
{{
    {RenderAsSignalParammeters(signal.Parameters)}
}}";
    }
}

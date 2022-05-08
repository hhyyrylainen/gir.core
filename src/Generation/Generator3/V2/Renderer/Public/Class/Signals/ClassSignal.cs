using System;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static partial class ClassSignal
{
    public static string Render(GirModel.Class cls, GirModel.Signal signal)
    {
        try
        {
            return $@"
#region {signal.GetPublicName()}
{RenderArgs(signal)}
{RenderArgsIndexer(cls, signal)}
{RenderDescriptor(cls, signal)}
{RenderEvent(cls, signal)}
#endregion
";
        }
        catch (Exception ex)
        {
            var message = $"Did not generate signal '{cls.Name}.{signal.GetPublicName()}': {ex.Message}";

            if (ex is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static string GetGenericArgs(GirModel.Class cls, GirModel.Signal signal)
    {
        return signal.Parameters.Any()
            ? $"{cls.Name}, {signal.GetArgsClassName()}"
            : cls.Name;
    }
}

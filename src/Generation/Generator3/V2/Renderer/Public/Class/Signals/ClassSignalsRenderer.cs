﻿using System;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.V2.Public;

public static class ClassSignalsRenderer
{
    public static string Render(GirModel.Class cls)
    {
        return $@"
using System;
using GObject;
using System.Runtime.InteropServices;

#nullable enable

namespace { cls.Namespace.GetPublicName() }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public partial class { cls.Name }
    {{
        {RenderObjectIndexer(cls)}
        {cls.Signals
            .Select(x => ClassSignal.Render(cls, x))
            .Join(Environment.NewLine)}
    }}
}}";
    }

    private static string RenderObjectIndexer(GirModel.Class cls)
    {
        return !cls.Signals.Any()
            ? string.Empty
            : @$"
/// <summary>
/// Indexer to connect with a SignalHandler&lt;{cls.Name}&gt;
/// </summary>
public SignalHandler<{cls.Name}> this[Signal<{cls.Name}> signal]
{{
    set => signal.Connect(this, value, true);
}}";
    }
}

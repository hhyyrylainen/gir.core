using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Renderer.V2.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class PublicConstantsGenerator : Generator<IEnumerable<GirModel.Constant>>
{
    public CodeUnit Generate(IEnumerable<GirModel.Constant> obj)
    {
        var project = obj.First().Namespace.GetCanonicalName();

        return obj
            .Where(IsValid)
            .Map(ConstantsRenderer.Render)
            .Map(source => new CodeUnit(project, "Constants", source));
    }

    private static bool IsValid(GirModel.Constant constant)
    {
        switch (constant.Type)
        {
            case GirModel.PrimitiveType:
                return IsValidPrimitiveType(constant);
            case GirModel.Bitfield:
                return true;
            default:
                Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't assign value '{constant.Value}' to unsupported type '{constant.Type.GetName()}'.");
                return false;
        }
    }

    private static bool IsValidPrimitiveType(GirModel.Constant constant)
    {
        var canParse = constant.Type switch
        {
            GirModel.Bool => bool.TryParse(constant.Value, out _),
            GirModel.Byte => byte.TryParse(constant.Value, out _),
            GirModel.Double => double.TryParse(constant.Value, out _),
            GirModel.Integer => int.TryParse(constant.Value, out _),
            GirModel.Long => long.TryParse(constant.Value, out _),
            GirModel.NativeUnsignedInteger => nuint.TryParse(constant.Value, out _),
            GirModel.Short => short.TryParse(constant.Value, out _),
            GirModel.SignedByte => sbyte.TryParse(constant.Value, out _),
            GirModel.String => true,
            GirModel.UnsignedInteger => uint.TryParse(constant.Value, out _),
            GirModel.UnsignedLong => ulong.TryParse(constant.Value, out _),
            GirModel.UnsignedShort => ushort.TryParse(constant.Value, out _),
            _ => false
        };

        if (canParse)
            return true;

        Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't convert value '{constant.Value}' to type '{constant.Type.GetName()}'.");

        return false;
    }
}

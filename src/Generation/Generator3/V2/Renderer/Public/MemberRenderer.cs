using Generator3.Converter;

namespace Generator3.Renderer.Public;

public static class MemberRenderer
{
    public static string Render(GirModel.Member member)
        => $"{ member.GetPublicName() } = { member.Value },";
}

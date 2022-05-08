using GirModel;

namespace Generator3.Renderer.V2;

public static class NullableRenderer
{
    //TODO Is this really a renderer? Model is referencing it!!
    //TODO There are copies of this code in the codebase
    public static string RenderNullable(this Nullable nullable)
    {
        return nullable.Nullable ? "?" : string.Empty;
    }
}

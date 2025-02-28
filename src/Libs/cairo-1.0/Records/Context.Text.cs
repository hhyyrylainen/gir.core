﻿namespace Cairo
{
    public partial class Context
    {
        // TODO add method using cairo_glyph_t
        // - cairo_show_glyphs()
        // - cairo_show_text_glyphs()
        // - cairo_glyph_extents()

        public void SelectFontFace(string family, FontSlant slant, FontWeight weight)
            => Internal.Context.SelectFontFace(Handle, family, slant, weight);

        public void SetFontSize(double size)
            => Internal.Context.SetFontSize(Handle, size);

        public void SetFontMatrix(Matrix matrix)
            => Internal.Context.SetFontMatrix(Handle, matrix.Handle);

        public void GetFontMatrix(Matrix matrix)
            => Internal.Context.GetFontMatrix(Handle, matrix.Handle);

        public void SetFontOptions(FontOptions options)
            => Internal.Context.SetFontOptions(Handle, options.Handle);

        public void GetFontOptions(FontOptions options)
            => Internal.Context.GetFontOptions(Handle, options.Handle);

        public void SetFontFace(FontFace font_face)
            => Internal.Context.SetFontFace(Handle, font_face.Handle);

        public FontFace GetFontFace()
            => new FontFace(Internal.Context.GetFontFace(Handle));

        public void SetScaledFont(ScaledFont scaled_font)
            => Internal.Context.SetScaledFont(Handle, scaled_font.Handle);

        public ScaledFont GetScaledFont()
            => new ScaledFont(Internal.Context.GetScaledFont(Handle));

        public void ShowText(string text)
            => Internal.Context.ShowText(Handle, text);

        public void FontExtents(out FontExtents extents)
            => Internal.Context.FontExtents(Handle, out extents);

        public void TextExtents(string text, out TextExtents extents)
            => Internal.Context.TextExtents(Handle, text, out extents);
    }
}

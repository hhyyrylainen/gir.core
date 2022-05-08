using System;
using System.Runtime.InteropServices;

namespace Cairo
{
    public partial class RectangleInt : IEquatable<RectangleInt>
    {
        public RectangleInt() :
            this(Internal.RectangleIntManagedHandle.Create(new Internal.RectangleIntData()))
        {
        }

        public override int GetHashCode() => Data.GetHashCode();

        public override bool Equals(object? obj) => Equals(obj as RectangleInt);

        public bool Equals(RectangleInt? other)
            => other != null && Data.Equals(other.Data);

        // Marshal the handle to / from a RectangleIntData struct.
        private Internal.RectangleIntData Data
        {
            get => Marshal.PtrToStructure<Internal.RectangleIntData>(Handle.DangerousGetHandle());
            set => Marshal.StructureToPtr(value, Handle.DangerousGetHandle(), false);
        }

        public int X
        {
            get => Data.X;
            set => Data = Data with { X = value };
        }

        public int Y
        {
            get => Data.Y;
            set => Data = Data with { Y = value };
        }

        public int Width
        {
            get => Data.Width;
            set => Data = Data with { Width = value };
        }

        public int Height
        {
            get => Data.Height;
            set => Data = Data with { Height = value };
        }
    }
}

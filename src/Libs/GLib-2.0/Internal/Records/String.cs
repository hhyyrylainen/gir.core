﻿using System;
using System.Runtime.InteropServices;

#nullable enable

namespace GLib.Internal
{

    public partial class String
    {
        public partial class Methods
        {
            public static void Free(IntPtr str)
            {
                //TODO: Free(str, true);
                throw new NotImplementedException();
            }
        }
    }
}

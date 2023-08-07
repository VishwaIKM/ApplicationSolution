﻿using System;

namespace VishwaDockLibNew.Enum
{
    [Flags]
    public enum DockSide
    {
        None = 0x0000,
        Left = 0x0001,
        Right = 0x0002,
        Top = 0x0004,
        Bottom = 0x0008,
        LeftRight = Left | Right,
        TopBottom = Top | Bottom,
        LeftBottom = Left | Bottom,
        TopRight = Top | Right,
        All = LeftRight | TopBottom
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VishwaDockLibNew.Interface
{
    public interface IDockView : IDisposable
    {
        IDockModel Model { get; }
        IDockView DockViewParent { get; }
    }
}
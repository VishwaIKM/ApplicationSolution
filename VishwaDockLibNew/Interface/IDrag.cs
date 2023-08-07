﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using VishwaDockLibNew.Enum;

namespace VishwaDockLibNew.Interface
{
    public interface IDragTarget : IAttcah
    {
        DockManager DockManager { get; }
        DragMode Mode { get; }
        void OnDrop(DragItem source);
        void HideDropWindow();
        void ShowDropWindow();
        void CloseDropWindow();
        void Update(Point mouseP);
        DropMode DropMode { get; set; }
    }

    public interface IAttcah
    {
        void AttachWith(IDockView source, AttachMode mode = AttachMode.Center);
    }
}
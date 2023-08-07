﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using VishwaDockLibNew.Enum;

namespace VishwaDockLibNew.Interface
{
    public interface IDropWindow
    {
        void Hide();
        void Show();
        void Close();
        void Update(Point mouseP);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace VishwaDockLibNew.ViewRender
{
    public abstract class BaseVisual : DrawingVisual
    {
        protected BaseVisual()
        {

        }

        public abstract void Update(Size size);
    }
}
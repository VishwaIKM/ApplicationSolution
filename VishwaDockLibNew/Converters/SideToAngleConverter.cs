﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VishwaDockLibNew.Enum;

namespace VishwaDockLibNew.Converters
{
    [ValueConversion(typeof(DockSide), typeof(double))]
    public class SideToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DockSide side = (DockSide)value;
            if (side == DockSide.Right)
                return 90.0;
            if (side == DockSide.Left)
                return 270;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

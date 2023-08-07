using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace VishwaDockLibNew.Interface
{
    public interface IDockSource
    {
        IDockControl DockControl { get; set; }
        string Header { get; }
        ImageSource Icon { get; }
    }

    public interface IDockDocSource : IDockSource
    {
     
        bool IsModified { get; set; }
        string FullFileName { get; }
        string FileName { get; }
        void Save();
        void ReLoad();
        bool AllowClose();
    }
}
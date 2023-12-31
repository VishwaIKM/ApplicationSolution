﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using VishwaDockLibNew.Enum;
using VishwaDockLibNew.Interface;
using VishwaDockLibNew.Model;

namespace VishwaDockLibNew.ViewRender
{
    public class LayoutContentControl : Control, INotifyPropertyChanged, IDisposable
    {
        static LayoutContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutContentControl), new FrameworkPropertyMetadata(typeof(LayoutContentControl)));
        }

        public LayoutContentControl()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(DockElement), typeof(LayoutContentControl));

        public DockElement Model
        {
            get { return (DockElement)GetValue(ModelProperty); }
            set
            {
                SetValue(ModelProperty, value);
            }
        }

        public void Dispose()
        {
            Model = null;
            DataContext = null;
        }
    }
}
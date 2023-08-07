﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VishwaDockLibNew.Enum;
using VishwaDockLibNew.Interface;
using VishwaDockLibNew.Model;

namespace VishwaDockLibNew.ViewRender
{
    public class AnchorSideGroupControl : BaseGroupControl
    {
        static AnchorSideGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnchorSideGroupControl), new FrameworkPropertyMetadata(typeof(AnchorSideGroupControl)));
            FocusableProperty.OverrideMetadata(typeof(AnchorSideGroupControl), new FrameworkPropertyMetadata(false));
        }

        internal AnchorSideGroupControl(ILayoutGroup model, double desiredWidth = Constants.DockDefaultWidthLength, double desiredHeight = Constants.DockDefaultHeightLength) : base(model, desiredWidth, desiredHeight)
        {
            
        }

        public override DragMode Mode
        {
            get
            {
                return DragMode.Anchor;
            }
        }

        public override void OnDrop(DragItem source)
        {
            if (DropMode == DropMode.Left
                || DropMode == DropMode.Right
                || DropMode == DropMode.Top
                || DropMode == DropMode.Bottom)
            {
                IDockView child;
                if (source.RelativeObj is BaseFloatWindow)
                {
                    child = (source.RelativeObj as BaseFloatWindow).Child;
                    (source.RelativeObj as BaseFloatWindow).DetachChild(child);
                }
                else child = source.RelativeObj as IDockView;

                DockManager.ChangeDockMode(child, (Model as ILayoutGroup).Mode);
                //must to changside
                DockManager.ChangeSide(child, Model.Side);

                LayoutGroupPanel panel;
                if (DockViewParent == null)
                {
                    var wnd = Parent as BaseFloatWindow;
                    wnd.DetachChild(this);
                    panel = new LayoutGroupPanel(Model.Side)
                    {
                        Direction = (DropMode == DropMode.Left || DropMode == DropMode.Right) ? Direction.Horizontal : Direction.Vertical,
                        DesiredWidth = wnd.ActualWidth,
                        DesiredHeight = wnd.ActualHeight,
                        IsAnchorPanel = true
                    };
                    wnd.DockManager = DockManager;
                    wnd.AttachChild(panel, AttachMode.None, 0);
                    panel._AttachChild(this, 0);
                }
                else panel = DockViewParent as LayoutGroupPanel;

                AttachTo(panel, child, DropMode);
            }
            else base.OnDrop(source);

            if (source.RelativeObj is BaseFloatWindow)
                (source.RelativeObj as BaseFloatWindow).Close();
        }

        public void AttachTo(LayoutGroupPanel panel, IDockView source, DropMode mode)
        {
            int index = panel.Children.IndexOf(this);
            switch (mode)
            {
                case DropMode.Left:
                    if (panel.Direction == Direction.Vertical)
                    {
                        var _subpanel = new LayoutGroupPanel(Model.Side)
                        {
                            Direction = Direction.Horizontal,
                            DesiredWidth = Math.Max(ActualWidth, Constants.DockDefaultWidthLength),
                            DesiredHeight = Math.Max(ActualHeight, Constants.DockDefaultHeightLength),
                            IsAnchorPanel = true
                        };
                        panel._DetachChild(this);
                        panel._AttachChild(_subpanel, Math.Min(index, panel.Count));
                        _subpanel._AttachChild(this, 0);
                        _subpanel.AttachChild(source, AttachMode.Left, 0);
                    }
                    else panel._AttachChild(source, index);
                    break;
                case DropMode.Top:
                    if (panel.Direction == Direction.Horizontal)
                    {
                        var _subpanel = new LayoutGroupPanel(Model.Side)
                        {
                            Direction = Direction.Vertical,
                            DesiredWidth = Math.Max(ActualWidth, Constants.DockDefaultWidthLength),
                            DesiredHeight = Math.Max(ActualHeight, Constants.DockDefaultHeightLength),
                            IsAnchorPanel = true
                        };
                        panel._DetachChild(this);
                        panel._AttachChild(_subpanel, Math.Min(index, panel.Count));
                        _subpanel._AttachChild(this, 0);
                        _subpanel.AttachChild(source, AttachMode.Top, 0);
                    }
                    else panel._AttachChild(source, index);
                    break;
                case DropMode.Right:
                    if (panel.Direction == Direction.Vertical)
                    {
                        var _subpanel = new LayoutGroupPanel(Model.Side)
                        {
                            Direction = Direction.Horizontal,
                            DesiredWidth = Math.Max(ActualWidth, Constants.DockDefaultWidthLength),
                            DesiredHeight = Math.Max(ActualHeight, Constants.DockDefaultHeightLength),
                            IsAnchorPanel = true
                        };
                        panel._DetachChild(this);
                        _subpanel._AttachChild(this, 0);
                        _subpanel.AttachChild(source, AttachMode.Right, 1);
                        panel._AttachChild(_subpanel, Math.Min(index, panel.Count));
                    }
                    else panel._AttachChild(source, index + 1);
                    break;
                case DropMode.Bottom:
                    if (panel.Direction == Direction.Horizontal)
                    {
                        var _subpanel = new LayoutGroupPanel(Model.Side)
                        {
                            Direction = Direction.Vertical,
                            DesiredWidth = Math.Max(ActualWidth, Constants.DockDefaultWidthLength),
                            DesiredHeight = Math.Max(ActualHeight, Constants.DockDefaultHeightLength),
                            IsAnchorPanel = true
                        };
                        panel._DetachChild(this);
                        _subpanel._AttachChild(this, 0);
                        _subpanel.AttachChild(source, AttachMode.Bottom, 1);
                        panel._AttachChild(_subpanel, Math.Min(index, panel.Count));
                    }
                    else panel._AttachChild(source, index + 1);
                    break;
            }
        }
    }
}
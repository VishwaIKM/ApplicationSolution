using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using YDock.Enum;
using YDock.Interface;
using YDock.Model;
using YDock.View;

namespace YDock
{
    public class DockControl : IDockControl
    {
        internal DockControl(IDockElement prototype)
        {
            _protoType = prototype;
            (_protoType as DockElement).DockControl = this;
            prototype.PropertyChanged += OnPrototypePropertyChanged;
        }

        private void OnPrototypePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged(_protoType, new PropertyChangedEventArgs(e.PropertyName));
        }

        #region ProtoType
        private IDockElement _protoType;

        public IDockElement ProtoType
        {
            get { return _protoType; }
        }
        #endregion

        #region Interface
        public int ID
        {
            get
            {
                return _protoType.ID;
            }
        }

        public string Title
        {
            get
            {
                return _protoType.Title;
            }
            set { _protoType.Title = value; }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _protoType.ImageSource;
            }
            set { _protoType.ImageSource = value; }
        }

        public object Content
        {
            get
            {
                return _protoType.Content;
            }
        }

        public DockSide Side
        {
            get
            {
                return _protoType.Side;
            }
        }

        public DockManager DockManager
        {
            get
            {
                return _protoType.DockManager;
            }
        }

        public double DesiredWidth
        {
            get
            {
                return _protoType.DesiredWidth;
            }

            set
            {
                _protoType.DesiredWidth = value;
            }
        }

        public double DesiredHeight
        {
            get
            {
                return _protoType.DesiredHeight;
            }

            set
            {
                _protoType.DesiredHeight = value;
            }
        }

        public double FloatLeft
        {
            get
            {
                return _protoType.FloatLeft;
            }

            set
            {
                _protoType.FloatLeft = value;
            }
        }

        public double FloatTop
        {
            get
            {
                return _protoType.FloatTop;
            }

            set
            {
                _protoType.FloatTop = value;
            }
        }

        public bool IsDocument
        {
            get { return _protoType.IsDocument; }
        }

        public DockMode Mode
        {
            get
            {
                return _protoType.Mode;
            }
        }

        public bool IsVisible
        {
            get { return _protoType.IsVisible; }
        }

        public bool IsActive
        {
            get { return _protoType.IsActive; }
        }

        public bool CanSelect
        {
            get { return _protoType.CanSelect; }
        }

        public ILayoutGroup Container
        {
            get { return _protoType.Container; }
        }

        public bool IsDocked => _protoType.IsDocked;

        public bool IsFloat => _protoType.IsFloat;

        public bool IsAutoHide => _protoType.IsAutoHide;

        public bool CanFloat
        {
            get
            {
                return _protoType == null ? false : _protoType.CanFloat;
            }
        }

     
        public bool CanDock
        {
            get
            {
                return _protoType == null ? false : _protoType.CanDock;
            }
        }

      
        public bool CanDockAsDocument
        {
            get
            {
                return _protoType == null ? false : _protoType.CanDockAsDocument;
            }
        }

        public bool CanSwitchAutoHideStatus
        {
            get
            {
                return _protoType == null ? false : _protoType.CanSwitchAutoHideStatus;
            }
        }

     
        public bool CanHide
        {
            get
            {
                return _protoType == null ? false : _protoType.CanHide;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

      
        public void Show(bool toActice = true)
        {
            if (IsVisible && IsActive) return;
            if (Mode == DockMode.Float)
            {
                if (!IsDocument)
                    ToFloat(toActice);
                else ToDockAsDocument(toActice);
            }
            else Container.ShowWithActive(_protoType, toActice);
        }

        public void Hide()
        {
            if (Content is IDockDocSource
                && !(Content as IDockDocSource).AllowClose())
                return;
            _protoType?.Hide();
        }

      
        public void ToFloat(bool isActive = true)
        {
            _protoType?.ToFloat(isActive);
        }

     
        public void ToDock(bool isActive = true)
        {
            _protoType?.ToDock(isActive);
        }

        public void ToDockAsDocument(bool isActive = true)
        {
            _protoType?.ToDockAsDocument(isActive);
        }

      
        public void ToDockAsDocument(int index, bool isActive = true)
        {
            _protoType?.ToDockAsDocument(index, isActive);
        }

       
        public void SwitchAutoHideStatus()
        {
            _protoType?.SwitchAutoHideStatus();
        }

        
        public void Close()
        {
            Hide();
        }

        public void SetActive(bool _isActive = true)
        {
            if (_isActive)
                _protoType.Container.ShowWithActive(_protoType, _isActive);
            else if(DockManager.ActiveElement == _protoType)
                DockManager.ActiveElement = null;
        }

        private bool _isDisposed = false;
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            DockManager?.RemoveDockControl(this);
            if (_protoType != null)
            {
                _protoType.PropertyChanged -= OnPrototypePropertyChanged;
                _protoType.Dispose();
                _protoType = null;
            }
        }
    }
}
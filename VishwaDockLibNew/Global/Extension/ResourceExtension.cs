﻿using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace VishwaDockLibNew.Global.Extension
{
    public class ResourceExtension : MarkupExtension, INotifyPropertyChanged
    {
        public ResourceExtension()
        {
            LanaguageChanged += OnLanaguageChanged;
        }

        public ResourceExtension(string key) : this()
        {
            Key = key;
        }

        private void OnLanaguageChanged(object sender, EventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }
        [ConstructorArgument("Key")]
        public string Key { get; set; }

        public string Value => Properties.Resources.ResourceManager.GetString(Key, Properties.Resources.Culture);

        public static event EventHandler LanaguageChanged = delegate { };

        public static void RaiseLanaguageChanged()
        {
            LanaguageChanged(null, new EventArgs());
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget? target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            Setter? setter = target.TargetObject as Setter;
            if (setter != null)
                return new Binding("Value") { Source = this, Mode = BindingMode.OneWay };
            else
            {
                Binding binding = new Binding("Value") { Source = this, Mode = BindingMode.OneWay };
                return binding.ProvideValue(serviceProvider);
            }
        }
    }
}

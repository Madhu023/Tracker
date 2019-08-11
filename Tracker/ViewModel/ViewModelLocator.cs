using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tracker.ViewModel
{
    public static class ViewModelLocator
    {

        public static bool GetAutoWireProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireProperty);
        }

        public static void SetAutoWireProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireProperty =
            DependencyProperty.RegisterAttached("AutoWire", typeof(bool), typeof(ViewModelBase), new PropertyMetadata(false, AutoWirePropertyChanged));

        private static void AutoWirePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var viewType = d.GetType();
            string str = viewType.FullName;
            str = str.Replace(".View.", ".ViewModel.");

            var viewTypeName = str;
            var viewModelTypeName = viewTypeName + "ViewModel";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}

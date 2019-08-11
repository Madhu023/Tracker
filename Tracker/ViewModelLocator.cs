using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tracker.ViewModel;

namespace Tracker
{
    public static class ViewModelLocator
    {

        public static bool GetMyProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(AutoWireProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireProperty =
            DependencyProperty.RegisterAttached("AutoWire", typeof(bool), typeof(ViewModelBase), new PropertyMetadata(0));
    }
}

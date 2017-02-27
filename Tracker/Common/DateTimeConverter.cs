using System;
using System.Globalization;
using System.Windows.Data;

namespace Tracker.Common
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)(value)).ToString("dd-MMM-yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDateTime(value.ToString());
        }
    }
}

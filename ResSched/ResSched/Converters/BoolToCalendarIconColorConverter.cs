using System;
using System.Globalization;
using Xamarin.Forms;

namespace ResSched.Converters
{
    public class BoolToCalendarIconColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return "Green";
            else
                return "Red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
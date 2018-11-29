using System;
using System.Globalization;
using Xamarin.Forms;

namespace ResSched.Converters
{
    public class MyReallyGreatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "hello")
                return "Blue";
            else
                return "Red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
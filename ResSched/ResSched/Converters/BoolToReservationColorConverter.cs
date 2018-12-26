using System;
using System.Globalization;
using Xamarin.Forms;

namespace ResSched.Converters
{
    public class BoolToReservationColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return "LightGray";
            else
                return "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
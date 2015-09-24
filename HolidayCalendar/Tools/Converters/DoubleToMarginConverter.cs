using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HolidayCalendar.Tools.Converters
{
    public class DoubleToMarginConverter : IValueConverter
    {
        /// <summary>
        /// returns left margin based on provided value 
        /// </summary>

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var marginWidth = value as double?;

            if (!marginWidth.HasValue)
            {
                return new Thickness(0);
            }

            return new Thickness(marginWidth.Value,0,0,0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

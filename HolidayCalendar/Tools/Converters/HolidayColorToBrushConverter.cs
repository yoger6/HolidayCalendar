using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DataLib.Model;

namespace HolidayCalendar.Tools.Converters
{
    public class HolidayColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = value as HolidayColor;
            if (color == null)
            {
                return new SolidColorBrush(Colors.White);
            }

            return new SolidColorBrush(color.Color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

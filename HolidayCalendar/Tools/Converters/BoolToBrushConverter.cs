using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HolidayCalendar.Tools.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                throw new ArgumentNullException("Both value and parameter must be provided.");
            }
            
            var boolValue = value as bool?;
            if (!boolValue.HasValue)
            {
                throw new ArgumentException("Value must be boolean.");
            }
            var colorParameter = parameter as Color?;
            if (!colorParameter.HasValue)
            {
                throw new ArgumentException("Parameter must be of type System.Windows.Media.Color");
            }

            return boolValue.Value ? new SolidColorBrush(colorParameter.Value) : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

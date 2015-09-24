using System;
using System.Globalization;
using System.Windows.Data;

namespace HolidayCalendar.Tools.Converters
{
    public class StringLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var stringToConvert = value.ToString();
            
            

            var intParameter = parameter as int?;
            if (intParameter.HasValue && intParameter.Value > 0 && intParameter.Value < stringToConvert.Length)
            {
                return stringToConvert.Substring(0, intParameter.Value);
            }

            return stringToConvert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

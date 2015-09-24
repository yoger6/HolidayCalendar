using System;
using System.Windows.Media;
using HolidayCalendar.Tools;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class BoolToBrushConverterTest
    {
        private BoolToBrushConverter _converter = new BoolToBrushConverter();
        private Color _correctParameter = Colors.Black;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsExceptionWhenValueIsNull()
        {
            GetConversionResult(null, _correctParameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsExceptionWhenParameterIsNull()
        {
            GetConversionResult(true, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionWhenParameterNotColor()
        {
            GetConversionResult(true, string.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionWhenValueIsNotBool()
        {
            GetConversionResult(string.Empty, _correctParameter);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertBackIsNotSupprted()
        {
            _converter.ConvertBack(null, typeof(Brush), null, null);
        }

        [TestMethod]
        public void ReturnsSolidColorBrushWithCorrectColorWhenValueIsTrue()
        {
            var result = GetConversionResult(true, _correctParameter) as SolidColorBrush;

            Assert.AreEqual(_correctParameter, result.Color);
        }

        [TestMethod]
        public void ReturnsUnsetWhenValueIsFalse()
        {
            var result = GetConversionResult(false, _correctParameter);

            Assert.AreEqual(result.ToString(), "{DependencyProperty.UnsetValue}");
        }

        private object GetConversionResult(object value, object parameter)
        {
            return _converter.Convert(value, typeof (Color), parameter, null);
        }
    }
}

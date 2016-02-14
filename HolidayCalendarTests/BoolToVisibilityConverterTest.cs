using System;
using System.Windows;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class BoolToVisibilityConverterTest
    {
        private BoolToVisibilityConverter _converter = new BoolToVisibilityConverter();

        [TestMethod]
        [ExpectedException(typeof (NotSupportedException))]
        public void ConvertBackIsNotSupported()
        {
            _converter.ConvertBack(null, null, null, null);
        }

        [TestMethod]
        public void ValueCannotBeConvertedRetunsNull()
        {
            var conversionResult = Convert("this isn't sparta, this is string");
            Assert.IsNull(conversionResult);
        }

        [TestMethod]
        public void SuccessfullConversionReturnsVisibility()
        {
            var conversionResult = Convert(true);

            Assert.IsTrue(Enum.IsDefined(typeof(Visibility), conversionResult));
        }

        private object Convert(object value)
        {
            return _converter.Convert(value, null, null, null);
        }
    }
}

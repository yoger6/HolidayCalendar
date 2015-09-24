using System.Windows;
using HolidayCalendar.Tools;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class DoubleToMarginValueTest
    {
        private DoubleToMarginConverter _converter = new DoubleToMarginConverter();

        [TestMethod]
        public void NullValueReturnsZeroThickness()
        {
            var thickness = _converter.Convert(null, typeof (Thickness), null, null) as Thickness?;

            Assert.AreEqual(0, thickness.Value.Left);
        }

        [TestMethod]
        public void DoubleValueReturnsThicknessWithThisValueAssignedToLeft()
        {
            var valueToConvert = 1.0;

            var thickness = _converter.Convert(valueToConvert, typeof (Thickness), null, null) as Thickness?;

            Assert.AreEqual(valueToConvert, thickness.Value.Left);
        }
    }
}

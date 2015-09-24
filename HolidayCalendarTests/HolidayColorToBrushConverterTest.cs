using System;
using System.Windows.Media;
using DataLib.Model;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class HolidayColorToBrushConverterTest
    {
        private HolidayColorToBrushConverter _converter;
        private HolidayColor _color;
        
        [TestInitialize]
        public void Initialize()
        {
            _converter = new HolidayColorToBrushConverter();
            _color = new HolidayColor(Colors.Black);
        }    

        [TestMethod]
        [ExpectedException(typeof (NotSupportedException))]
        public void ConvertingBackThrowsException()
        {
            _converter.ConvertBack(Brushes.Black, typeof(Color), null, null);
        }
        

        [TestMethod]
        public void ConvertingNullValueReturnsWhiteBrush()
        {
            var brush = _converter.Convert(null, typeof (Brush), null, null) as SolidColorBrush;

            Assert.AreEqual(Colors.White, brush.Color);
        }

        [TestMethod]
        public void ConvertingColorReturnsBrushOfThisColor()
        {
            var brush = (SolidColorBrush)_converter.Convert(_color, typeof (SolidColorBrush), null, null);

            Assert.AreEqual(_color.Color, brush.Color);
        }
    }
}

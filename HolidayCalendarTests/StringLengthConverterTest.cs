using HolidayCalendar.Tools;
using HolidayCalendar.Tools.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class StringLengthConverterTest
    {
        StringLengthConverter _converter = new StringLengthConverter();
        string _stringToConvert = "I am string.";

        [TestMethod]
        public void NullValueReturnsEmptyString()
        {
            var convertedString = _converter.Convert(null, typeof (string), null, null) as string;
            Assert.AreEqual(string.Empty, convertedString);
        }

        [TestMethod]
        public void CorrectStringWithoutParameterReturnsThisString()
        {

            var convertedString = _converter.Convert(_stringToConvert, typeof (string), null, null);
            
            Assert.AreEqual(_stringToConvert, convertedString);
        }

        [TestMethod]
        public void CorrectStringWithIntParameterReturnsStringOfThatLength()
        {
            var converterParameter = 1;
            var convertedString = _converter.Convert(_stringToConvert, typeof(string), converterParameter, null) as string;

            Assert.AreEqual(converterParameter, convertedString.Length);
        }

        [TestMethod]
        public void CorrectStringWithIntParameterBiggerThanItsLengthReturnsOriginalString()
        {
            var converterParameter = 30;
            var convertedString = _converter.Convert(_stringToConvert, typeof(string), converterParameter, null) as string;

            Assert.AreEqual(_stringToConvert.Length, convertedString.Length);
        }
    }
}

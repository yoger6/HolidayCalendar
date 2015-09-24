using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar = HolidayCalendar.Tools.Calendar;

namespace HolidayCalendarTests
{
    [TestClass]
    public class CalendarTest
    {
        private Calendar _calendar = new Calendar();
        private int _year = 2015;

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GettingDaysOfIncorrectDateThrowException()
        {
            var month = 13;

            _calendar.SetDate(_year, month);

            var days = _calendar.Days;
        }
        
        [TestMethod]
        public void SpecifiedMonthHasCorrectNumberOfDays()
        {
            var month = 8;

            _calendar.SetDate(_year, month);

            Assert.AreEqual(31, _calendar.Days.Count);
        }

        [TestMethod]
        public void SpecifedDateIsCorrectDayOfWeek()
        {
            var month = 8;
            _calendar.SetDate(_year, month);

            Assert.AreEqual(_calendar.Days.First().Value, DayOfWeek.Saturday);
        }

        [TestMethod]
        public void CallingNextMonthIncreasesMonthValue()
        {
            _calendar.SetDate(2015, 1);

            _calendar.NextMonth();

            Assert.AreEqual(2, _calendar.Month);
        }

        [TestMethod]
        public void IncreasingMonthInDecemberResultsInJanuary()
        {
            _calendar.SetDate(_year, 12);

            _calendar.NextMonth();

            Assert.AreEqual(1, _calendar.Month);
        }

        [TestMethod]
        public void PreviousMonthDecreasesMonthValue()
        {
            _calendar.SetDate(2015, 2);

            _calendar.PreviousMonth();

            Assert.AreEqual(1, _calendar.Month);
        }

        [TestMethod]
        public void DecreasingMonthInJanuaryResultsInDecember()
        {
            _calendar.SetDate(_year, 1);

            _calendar.PreviousMonth();

            Assert.AreEqual(12, _calendar.Month);
        }

        [TestMethod]
        public void MonthNameIsCorrespondingItsNumber()
        {
            var monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

            _calendar.SetDate(2015,12);

            Assert.AreEqual(monthNames[11], _calendar.MonthName);
        }
    }
}

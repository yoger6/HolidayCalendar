using System.Linq;
using DataLib.Model;
using HolidayCalendar.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class HolidayApplierTest
    {
        private Employee _employee;
        private HolidayApplier _applier;

        [TestInitialize]
        public void Initialize()
        {
            _employee = new Employee();
            _applier = new HolidayApplier(_employee);
        }

        [TestMethod]
        public void AssigningHolidayReasonToSelectedDayAddsItForUser()
        {
            var reason = new HolidayReason();
            var day = new Day();

            _applier.ApplyHoliday(reason, new [] {day});

            Assert.AreEqual(reason, _employee.Holidays.First().HolidayReason);
        }

        [TestMethod]
        public void AssigningNullReasonRemovesExistingHolidays()
        {
            var day = new Day();
            _employee.Holidays.Add(day);
            
            _applier.ApplyHoliday(null, new[] { day });

            Assert.AreEqual(0, _employee.Holidays.Count);
        }

        [TestMethod]
        public void AssigningHolidayWhenUserAlreadyHasSomeSetForTheDateOverwritesIt()
        {
            var day = new Day();
            _employee.Holidays.Add(day);
            var newHoliday = new HolidayReason();

            _applier.ApplyHoliday(newHoliday, new []{day} );
        
            Assert.AreEqual(1, _employee.Holidays.Count);
            Assert.AreEqual(newHoliday, _employee.Holidays.First().HolidayReason);
        }
    }
}

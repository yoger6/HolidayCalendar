using System;
using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.Tools;
using HolidayCalendar.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class EmployeeDaysViewModelTests
    {
        private Employee _employee;
        private EmployeeDayViewModelObservableCollection _dayViewModelObservableCollection;
        private List<Day> _holidays;
        private Calendar _calendar;
        private Day _sampleHoliday;

        [TestInitialize]
        public void Initialize()
        {
            _calendar = new Calendar();
            _calendar.SetDate(2015, 9);
            _holidays = new List<Day>();
            _employee = new Employee {FirstName="Joe", FamilyName = "Doe", Holidays = _holidays};
            _dayViewModelObservableCollection = new EmployeeDayViewModelObservableCollection(_employee, _calendar);
            _sampleHoliday = new Day { Date = new DateTime(2015, 8, 15), 
                                       HolidayReason = new HolidayReason() };

        }

        [TestMethod]
        public void ItemsInCollectionRepresentDaysOfSelectedMonthInCalendar()
        {
            _calendar.SetDate(2015, 10);

            Assert.AreEqual(_calendar.Days.First().Key, _dayViewModelObservableCollection.First().Date);
        }

        [TestMethod]
        public void ItemsInCollectionInheritHolidaysFromEmployeeHolidayList()
        {
             _holidays.Add(_sampleHoliday);

            _calendar.SetDate(2015,8);
            var matchingDayFromCollection = _dayViewModelObservableCollection.First(x => x.Date == _sampleHoliday.Date);
            Assert.AreEqual(_sampleHoliday.HolidayReason, matchingDayFromCollection.HolidayReason);
        }

        [TestMethod]
        public void ChangingHolidayReasonOnViewModelIsReflectedInEmployeeList()
        {
            _holidays.Add(_sampleHoliday);
            _calendar.SetDate(2015,8);

            var dayViewModelWithExistingHoliday = _dayViewModelObservableCollection.First(x => x.Date == _sampleHoliday.Date);
            var newHolidayReason = new HolidayReason {FullName = "New"};
            dayViewModelWithExistingHoliday.HolidayReason = newHolidayReason;

            Assert.AreEqual(_sampleHoliday.HolidayReason.FullName, newHolidayReason.FullName);
        }

        [TestMethod]
        public void SettingHolidayReasonForDayEmployeeDoesntHaveCreatesItForHim()
        {
            _calendar.SetDate(2015,8);
            _dayViewModelObservableCollection.First().HolidayReason = new HolidayReason();

            Assert.AreEqual(1, _holidays.Count);
        }

        [TestMethod]
        public void RemovingExistingHolidayFromViewModelRemovesThatDayFromEmployee()
        {
            _holidays.Add(_sampleHoliday);
            _calendar.SetDate(2015, 8);

            var dayWithExistingHoliday = _dayViewModelObservableCollection.First(x => x.Date == _sampleHoliday.Date);
            dayWithExistingHoliday.HolidayReason = null;

            Assert.AreEqual(0, _holidays.Count);
        }
    }
}

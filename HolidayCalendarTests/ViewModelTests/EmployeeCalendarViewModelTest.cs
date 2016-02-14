using System;
using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.Tools;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class EmployeeCalendarViewModelTest
    {
        private Calendar _calendar;
        private Employee _employee;
        private List<Day> _holidays;
        private EmployeeCalendarViewModel _employeeCalendar;

        [TestInitialize]
        public void Initialize()
        {
            _calendar = new Calendar();
            _calendar.SetDate(2015, 7);
            _holidays = new List<Day>();
            _employee = new Employee {Holidays = _holidays};
            _employeeCalendar = new EmployeeCalendarViewModel(_employee, _calendar, true, new EmployeeRepositoryStub());
        }
        

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectingDaysWithParameterThatIsNotIEnumerableThrowsException()
        {
            _employeeCalendar.SelectionChangedCommand.Execute("string won't work here");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void SelectingDaysWithInvalidThatContainsIncorrectTypeOfObjectsThrowsException()
        {
            _employeeCalendar.SelectionChangedCommand.Execute(new object[] {1,2,3});
        }

        [TestMethod]
        public void SelectingDaysUpdatesSelectedDaysList()
        {
            var daysThatShouldBeSelected = new List<Day>();

            _employeeCalendar.SelectionChangedCommand.Execute(daysThatShouldBeSelected);

            CollectionAssert.AreEquivalent(daysThatShouldBeSelected, _employeeCalendar.SelectedDays);
        }

        [TestMethod]
        public void ExecutingClearHolidayCommandRemovesSelectedDaysFromEmployee()
        {
            var testDay = new Day {Date = new DateTime(2015, 8, 1), HolidayReason = new HolidayReason()};
            _holidays.Add(testDay);
            _calendar.SetDate(2015,8);
            _employeeCalendar.SelectedDays.Add(_employeeCalendar.EmployeeDays.First(x=>x.Date == testDay.Date));
            _employeeCalendar.ClearHolidayCommand.Execute(null);

            Assert.AreEqual(0, _holidays.Count);
        }

        [TestMethod]
        public void ApplyingHolidayToSelectedDaysAddsHolidayToEmployee()
        {
            _calendar.SetDate(2015,8);
            _employeeCalendar.SelectionChangedCommand.Execute(new List<DayViewModel> { _employeeCalendar.EmployeeDays.First() });
            _employeeCalendar.MarkDayCommand.Execute(new HolidayReason {Id = 1});

            Assert.AreEqual(1, _holidays.Count);
        }

        [TestMethod]
        public void UnauthorizedEmployeeCannotPerformChanges()
        {
            _employeeCalendar = new EmployeeCalendarViewModel(_employee, _calendar, false, new EmployeeRepositoryStub());

            Assert.IsFalse(_employeeCalendar.SelectionChangedCommand.CanExecute(null));
            Assert.IsFalse(_employeeCalendar.MarkDayCommand.CanExecute(null));
            Assert.IsFalse(_employeeCalendar.ClearHolidayCommand.CanExecute(null));
        }
    }
}

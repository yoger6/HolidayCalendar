using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class CalendarViewModelTest
    {
        private List<Employee> _employees;
        private HolidayCalendarViewModel _holidayCalendar;
        
        [TestInitialize]
        public void Initialize()
        {
            _employees = new List<Employee>
            {
                new Employee {FirstName = "Andy", FamilyName = "Dee"},
                new Employee {FirstName = "Johnny", FamilyName = "Zor"},
                new Employee {FirstName = "Zardoz", FamilyName = "Ohm"}
            };

            SettingsHelper.SaveConnectionString("Data Source=YOGER-SUPERMAN\\SQLEXPRESS;Initial Catalog=HolidayCalendar;Integrated Security=True;Connect Timeout=5");
            _holidayCalendar = new HolidayCalendarViewModel(_employees.First(), new EmployeeRepositoryStub(_employees));
         }

        [TestMethod]
        public void EmployeeCalendarsAreBeingGeneratedFromEmployeeRepository()
        {
            _holidayCalendar.FillWithEmployeeData();
            Assert.AreEqual(_employees.Count, _holidayCalendar.EmployeeCalendars.Count);
        }

        [TestMethod]
        public void OddEmployeeCalndarIsMarkedAsDistinct()
        {
            _holidayCalendar.FillWithEmployeeData();
            var shouldntBeDistinct = _holidayCalendar.EmployeeCalendars[0].IsDistinct;
            var shouldBeDistinct = _holidayCalendar.EmployeeCalendars[1].IsDistinct;
        
            Assert.IsTrue(shouldBeDistinct);
            Assert.IsFalse(shouldntBeDistinct);
        }

        [TestMethod]
        public void CommandsUsingCalendarTriggerItsDateChangeEvent()
        {
            var eventFiredTimes = 0;
            _holidayCalendar.Calendar.DateChanged += (sender, pair) => eventFiredTimes++;
            _holidayCalendar.NextMonthCommand.Execute(null);
            _holidayCalendar.PreviousMonthCommand.Execute(null);
            _holidayCalendar.NextYearCommand.Execute(null);
            _holidayCalendar.PreviousYearCommand.Execute(null);

            Assert.AreEqual(4, eventFiredTimes);
        }
    }
}

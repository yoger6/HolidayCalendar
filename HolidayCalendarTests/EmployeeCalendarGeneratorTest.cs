using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.Tools;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests
{
    [TestClass]
    public class EmployeeCalendarGeneratorTest
    {
        private EmployeeCalendarGenerator _generator;
        private EmployeeRepositoryStub _employeeRepository;
        private List<Employee> _employees;

        [TestInitialize]
        public void Initialize()
        {
            _employees = new List<Employee>
            {
                new Employee(),
                new Employee(),
                new Employee()
            };
            _employeeRepository = new EmployeeRepositoryStub(_employees);
            _generator = new EmployeeCalendarGenerator(_employeeRepository, new Calendar(), new MainViewModel());
            SettingsHelper.SaveConnectionString("Data Source=YOGER-SUPERMAN\\SQLEXPRESS;Initial Catalog=HolidayCalendar;Integrated Security=True;Connect Timeout=5");
        }

        [TestMethod]
        public void ReturnsCalendarsForEachEmployeeFromRepository()
        {
            var calendars = _generator.GetCalendars(_employees.First());

            Assert.AreEqual(3, calendars.Count());
        }

        [TestMethod]
        public void SpecifiedEmployeeIsAuthorizedForEdit()
        {
            var employeeToAuthorize = _employees.First();

            var calendars = _generator.GetCalendars(employeeToAuthorize);

            var calendarOfAuthorizedEmployee = calendars.FirstOrDefault(x => x.Owner == employeeToAuthorize);

            Assert.IsTrue(calendarOfAuthorizedEmployee.IsAuthorized);
        }
    }
}

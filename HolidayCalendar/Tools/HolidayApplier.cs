using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.ViewModel;

namespace HolidayCalendar.Tools
{
    public class HolidayApplier
    {
        private readonly Employee _employee;

        public HolidayApplier(Employee employee)
        {
            _employee = employee;
        }

        public void ApplyHoliday(HolidayReason reason, IList<Day> days)
        {
            foreach (var date in days)
            {
                ApplyHoliday(reason,date);
            }
        }

        private void ApplyHoliday(HolidayReason reason, Day day)
        {
            day.HolidayReason = reason;

            var employeesDay = GetEmployeesDay(day);
            if (reason == null)
            {
                _employee.Holidays.Remove(employeesDay);
            }
            else if (employeesDay != null)
            {
                employeesDay.HolidayReason = reason;
            }
            else
            {
                _employee.Holidays.Add(day);
            }
        }

        private Day GetEmployeesDay(Day day)
        {
            return _employee.Holidays.SingleOrDefault(x=>x.Date == day.Date);
        }
    }
}
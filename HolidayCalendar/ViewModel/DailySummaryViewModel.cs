using System.Collections.ObjectModel;
using HolidayCalendar.Misc;
using HolidayCalendar.Tools;
using System.Linq;

namespace HolidayCalendar.ViewModel
{
    public class DailySummaryViewModel : ObservableObject
    {
        private readonly Calendar _calendar;
        private readonly ObservableCollection<EmployeeCalendarViewModel> _employeeCalendars;

        public ObservableCollection<int> Summaries { get; }


        public DailySummaryViewModel(Calendar calendar, ObservableCollection<EmployeeCalendarViewModel> employeeCalendars)
        {
            Summaries = new ObservableCollection<int>();
            _employeeCalendars = employeeCalendars;
            _calendar = calendar;

            SetTriggersToCallCalculateSummary();
            CalculateSummary();
        }

        private void SetTriggersToCallCalculateSummary()
        {
            _calendar.DateChanged += (sender, args) => CalculateSummary();

            foreach (var employeeCalendar in _employeeCalendars)
            {
                employeeCalendar.EmployeeCalendarChanged += (sender, args) => CalculateSummary();
            }
        }

        private void CalculateSummary()
        {
            Summaries.Clear();

            for (int i = 0; i < _calendar.Days.Count; i++)
            {
                var dailySum = _employeeCalendars.Count(employeeCalendar => employeeCalendar.EmployeeDays[i].HolidayReason != null);
                Summaries.Add(dailySum);
            }
        }
    }
}

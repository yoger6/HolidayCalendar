using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class EmployeeDayViewModelObservableCollection : ObservableCollection<DayViewModel>
    {
        private readonly Calendar _calendar;
        private readonly Employee _employee;

        public EmployeeDayViewModelObservableCollection(Employee employee, Calendar calendar)
        {
            _calendar = calendar;
            _employee = employee;
            _calendar.DateChanged += (sender, args) => GenerateDaysForSelectedMonth();
            GenerateDaysForSelectedMonth();
        }

        private void GenerateDaysForSelectedMonth()
        {
            this.Clear();
            foreach (var dayOfWeek in _calendar.Days)
            {
                var employeesHoliday = GetHolidayFromEmployee(dayOfWeek.Key);
                var dayViewModelBaseOnDay = employeesHoliday == null ? new DayViewModel(dayOfWeek.Key) 
                                                                     : new DayViewModel(employeesHoliday);
                this.Add(dayViewModelBaseOnDay);
                dayViewModelBaseOnDay.ReasonChanged += SynchroniseChangedReasonWithModel;
            }
        }

        private void SynchroniseChangedReasonWithModel(object sender, EventArgs eventArgs)
        {
            var changedDayViewModel = sender as DayViewModel;
            if(changedDayViewModel == null) throw new ArgumentNullException(nameof(sender), "DayViewMoel that fired this event is no more");
            var correspondingEmployeesDay = _employee.Holidays.FirstOrDefault(x => x.Date == changedDayViewModel.Date);

            if (changedDayViewModel.HolidayReason == null)
            {
                _employee.Holidays.Remove(correspondingEmployeesDay);
            }
            else
            {
                if (correspondingEmployeesDay != null)
                {
                    correspondingEmployeesDay.HolidayReason = changedDayViewModel.HolidayReason;
                }
                else
                {
                    _employee.Holidays.Add(new Day
                    {
                        Date = changedDayViewModel.Date,
                        HolidayReason=changedDayViewModel.HolidayReason
                    });
                }
            }
        }
        
        private Day GetHolidayFromEmployee(DateTime date)
        {
            return _employee.Holidays.FirstOrDefault(x => x.Date == date);
        }
    }
}

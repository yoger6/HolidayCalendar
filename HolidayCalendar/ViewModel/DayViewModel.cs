using System;
using DataLib.Model;
using HolidayCalendar.Misc;

namespace HolidayCalendar.ViewModel
{
    public class DayViewModel : ObservableObject
    {
        public event EventHandler ReasonChanged; 
        
        private HolidayReason _holidayReason;

        public DateTime Date { get; }

        public HolidayReason HolidayReason
        {
            get { return _holidayReason; }
            set
            {
                if (Equals(value, _holidayReason)) return;
                _holidayReason = value;
                OnPropertyChanged();
                OnReasonChanged();
            }
        }

        public DayViewModel(Day day)
        {
            Date = day.Date;
            _holidayReason = day.HolidayReason;
        }

        public DayViewModel(DateTime date)
        {
            Date = date;
        }

        protected virtual void OnReasonChanged()
        {
            ReasonChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

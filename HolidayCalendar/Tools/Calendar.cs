using System;
using System.Collections.Generic;
using System.Globalization;
using HolidayCalendar.Misc;

namespace HolidayCalendar.Tools
{
    public class Calendar : ObservableObject
    {
        private readonly GregorianCalendar _calendar = new GregorianCalendar();
        private int _year;
        private int _month;
        
        public int Year
        {
            get { return _year; }
            set
            {
                if (value == _year) return;
                _year = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Days));
                OnDateChanged();
            }
        }
        public int Month
        {
            get { return _month; }
            private set
            {
                if (value == _month) return;
                _month = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Days));
                OnPropertyChanged(nameof(MonthName));
                OnDateChanged();
            }
        }
        public string MonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
        public Dictionary<DateTime, DayOfWeek> Days
        {
            get
            {
                var numberOfDays = _calendar.GetDaysInMonth(Year, Month);
            
                var days = new Dictionary<DateTime,DayOfWeek>();

                for (int i = 1; i < numberOfDays + 1; i++)
                {
                    var date = new DateTime(Year, Month, i);
                    
                    days.Add(date, date.DayOfWeek);
                }

                return days;
            }
        }

        public event EventHandler DateChanged; 

        public Calendar()
        {
            var today = DateTime.Today;
            _year = today.Year;
            _month = today.Month;
        }


        public void SetDate(int year, int month)
        {
            Year = year;
            Month = month;
        }
        
        public void NextMonth()
        {
            if (IsLastMonth())
            {
                Month = 1;
            }
            else
            {
                Month++;
            }
        }

        public void PreviousMonth()
        {
            if (IsFirstMonth())
            {
                Month = 12;
            }
            else
            {
                Month--;
            }
        }
        
        private bool IsLastMonth()
        {
            return Month == 12;
        }

        private bool IsFirstMonth()
        {
            return Month == 1;
        }

        protected virtual void OnDateChanged()
        {
            DateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

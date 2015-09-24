using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;
using Calendar = HolidayCalendar.Tools.Calendar;

namespace HolidayCalendar.ViewModel
{
    public class EmployeeCalendarViewModel : ObservableObject
    {
        public event EventHandler EmployeeCalendarChanged;

        private Calendar _calendar;
        public Employee _employee;
        private readonly bool _isAuthorized;
        private bool _isDistinct;
        private IHolidayReasonRepository _reasonRepository;
        private ICommand _markDayCommand;
        private ICommand _selectionChangedCommand;
        private ICommand _clearHolidayCommand;

        public List<DayViewModel> SelectedDays { get; private set; }
        public List<HolidayReason> HolidayReasons { get; }
        public EmployeeDayViewModelObservableCollection EmployeeDays { get; }
        public string EmployeeDisplayName => string.Format("{0} {1}", _employee.FamilyName, _employee.FirstName);
        public bool IsDistinct
        {
            get { return _isDistinct; }
            set
            {
                if (value == _isDistinct) return;
                _isDistinct = value;
                OnPropertyChanged();
            }
        }
        public bool IsAuthorized => _isAuthorized;
        public ICommand MarkDayCommand
        {
            get
            {
                if (_markDayCommand == null)
                {
                    _markDayCommand = new RelayCommand(MarkDay, o => _isAuthorized);
                }

                return _markDayCommand;
            }
        }
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                {
                    _selectionChangedCommand = new RelayCommand(SelectionChanged, o => _isAuthorized);
                }

                return _selectionChangedCommand;
            }
        }
        public ICommand ClearHolidayCommand
        {
            get
            {
                if (_clearHolidayCommand == null)
                {
                    _clearHolidayCommand = new RelayCommand(MarkDay, o => _isAuthorized);
                }

                return _clearHolidayCommand;
            }
        }

        public EmployeeCalendarViewModel(Employee employee, Calendar calendar, bool isAuthorizedToEdit)
        {
            _employee = employee;
            _calendar = calendar;
            _isAuthorized = isAuthorizedToEdit;

            EmployeeDays = new EmployeeDayViewModelObservableCollection(_employee, _calendar);
            SelectedDays = new List<DayViewModel>();

            _reasonRepository = new HolidayReasonRepository();
            HolidayReasons = _reasonRepository.GetAll();
        }

        private void SelectionChanged(object obj)
        {
            var selectedItemsList = obj as IEnumerable<object>;

            if (selectedItemsList == null)
            {
                throw new ArgumentException("Comand parameter must be IEnumerable.");
            }

            var selectedDayList = selectedItemsList.Cast<DayViewModel>().ToList();
            
            SelectedDays = selectedDayList;
        }
        
        private void MarkDay(object holidayReasonParameter)
        {
            var holidayReason = holidayReasonParameter as HolidayReason;
            UpdateSelectedDaysWithReason(holidayReason);
            OnEmployeeCalendarChanged();
        }
        
        private void UpdateSelectedDaysWithReason(HolidayReason reason)
        {
            foreach (var day in SelectedDays)
            {
                day.HolidayReason = reason;
            }
        }

        protected virtual void OnEmployeeCalendarChanged()
        {
            EmployeeCalendarChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

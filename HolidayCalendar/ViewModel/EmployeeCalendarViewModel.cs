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
    public class EmployeeCalendarViewModel : ChildViewModel
    {
        public event EventHandler EmployeeCalendarChanged;

        private Calendar _calendar;
        public Employee Owner;
        private readonly bool _isAuthorized;
        private readonly IEmployeeRepository _employeeRepository;
        private bool _isDistinct;
        private ICommand _markDayCommand;
        private ICommand _selectionChangedCommand;
        private ICommand _clearHolidayCommand;
        private ICommand _editEmployeeCommand;

        public List<DayViewModel> SelectedDays { get; private set; }
        public List<HolidayReason> HolidayReasons { get; set; }
        public EmployeeDayViewModelObservableCollection EmployeeDays { get; }

        public string EmployeeDisplayName => string.Format("{0} {1}", Owner.FamilyName, Owner.FirstName);
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
                    _markDayCommand = new RelayCommand(MarkDayExecute, o => _isAuthorized);
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
                    _selectionChangedCommand = new RelayCommand(SelectionChangedExecute, o => _isAuthorized);
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
                    _clearHolidayCommand = new RelayCommand(MarkDayExecute, o => _isAuthorized);
                }

                return _clearHolidayCommand;
            }
        }
        public ICommand EditEmployeeCommand
        {
            get {
                if (_editEmployeeCommand == null)
                {
                    _editEmployeeCommand = new RelayCommand(EditEmployeeExecute, o => _isAuthorized);
                }
                return _editEmployeeCommand;
            }
        }


        public EmployeeCalendarViewModel(Employee owner, Calendar calendar, bool isAuthorizedToEdit, IEmployeeRepository employeeRepository)
        {
            Owner = owner;
            _calendar = calendar;
            _isAuthorized = isAuthorizedToEdit;
            _employeeRepository = employeeRepository;

            EmployeeDays = new EmployeeDayViewModelObservableCollection(Owner, _calendar);
            SelectedDays = new List<DayViewModel>();
        }


        private void SelectionChangedExecute(object obj)
        {
            var selectedItemsList = obj as IEnumerable<object>;

            if (selectedItemsList == null)
            {
                throw new ArgumentException("Comand parameter must be IEnumerable.");
            }

            var selectedDayList = selectedItemsList.Cast<DayViewModel>().ToList();
            
            SelectedDays = selectedDayList;
        }
        
        private void MarkDayExecute(object holidayReasonParameter)
        {
            var holidayReason = holidayReasonParameter as HolidayReason;
            UpdateSelectedDaysWithReason(holidayReason);
            OnEmployeeCalendarChanged();
            _employeeRepository.Update(Owner);
        }

        private void EditEmployeeExecute(object obj)
        {
            var viewModel = new EditEmployeeViewModel(Owner, _employeeRepository, MainViewModel);
            viewModel.ViewModelClosed += (sender, args) => OnPropertyChanged(nameof(EmployeeDisplayName));
            MainViewModel.LoadUtilityViewModel(viewModel);
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

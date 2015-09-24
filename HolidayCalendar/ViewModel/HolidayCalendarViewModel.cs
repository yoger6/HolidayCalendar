using System.Collections.ObjectModel;
using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    /// <summary>
    /// Generates EmployeeCalendars for displaying rows in Calendar
    /// Provides summary view model for each day calculation
    /// </summary>
    public class HolidayCalendarViewModel : ChildViewModel
    {
        private ObservableCollection<EmployeeCalendarViewModel> _employeeCalendars;
        private IEmployeeRepository _employeeRepository;
        
        public Calendar Calendar { get; }
        public DailySummaryViewModel DailySummary { get; }
        public ObservableCollection<EmployeeCalendarViewModel> EmployeeCalendars
        {
            get { return _employeeCalendars; }
            set
            {
                if (Equals(value, _employeeCalendars)) return;
                _employeeCalendars = value;
                OnPropertyChanged();
            }
        }

        private ICommand _nextMonthCommand;
        private ICommand _previousMonthCommand;
        private ICommand _nextYearCommand;
        private ICommand _previousYearCommand;

        public ICommand NextMonthCommand
        {
            get
            {
                if (_nextMonthCommand == null)
                {
                    _nextMonthCommand = new RelayCommand(o => Calendar.NextMonth());
                }
                return _nextMonthCommand;
            }
        }
        public ICommand PreviousMonthCommand
        {
            get
            {
                if (_previousMonthCommand == null)
                {
                    _previousMonthCommand = new RelayCommand(o => Calendar.PreviousMonth());
                }
                return _previousMonthCommand;
            }
        }
        public ICommand NextYearCommand
        {
            get
            {
                if (_nextYearCommand == null)
                {
                    _nextYearCommand = new RelayCommand(o => Calendar.Year++);
                }
                return _nextYearCommand;
            }
        }
        public ICommand PreviousYearCommand
        {
            get
            {
                if (_previousYearCommand == null)
                {
                    _previousYearCommand = new RelayCommand(o => Calendar.Year--);
                }
                return _previousYearCommand;
            }
        }

        public HolidayCalendarViewModel() : this(new Employee()) { }
        public HolidayCalendarViewModel(Employee employee)
        :this(employee, new EmployeeRepository())
        {
        }
        public HolidayCalendarViewModel(Employee employee, IEmployeeRepository repository)
        {
            Calendar = new Calendar();
            SetupEmployees(employee, repository);
            DailySummary = new DailySummaryViewModel(Calendar, EmployeeCalendars);
        }

        private void SetupEmployees(Employee employee, IEmployeeRepository repository)
        {
            _employeeRepository = repository;   
            EmployeeCalendars = new ObservableCollection<EmployeeCalendarViewModel>();
            CreateEmployeeCalendars(employee);
        }
        
        private void CreateEmployeeCalendars(Employee loggedEmployee)
        {
            var employees = _employeeRepository.GetAll();
            foreach (var employee in employees)
            {
                var isAuthorized = employee.FamilyName.Equals(loggedEmployee.FamilyName);
                var employeeCalendar = new EmployeeCalendarViewModel(employee, Calendar, isAuthorized);
                EmployeeCalendars.Add(employeeCalendar);
            }

            MarkRowsAsDistinct();
        }
        
        private void MarkRowsAsDistinct()
        {
            for (int i = 0; i < _employeeCalendars.Count; i++)
            {
                _employeeCalendars[i].IsDistinct = i % 2 != 0;
            }
        }
    }
}

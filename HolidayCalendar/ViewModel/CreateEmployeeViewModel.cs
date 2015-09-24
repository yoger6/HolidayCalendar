using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;

namespace HolidayCalendar.ViewModel
{
    public class CreateEmployeeViewModel : ChildViewModel
    {
        private readonly Employee _employee;
        private readonly IEmployeeRepository _repository;
        private ICommand _saveEmployeeCommand;
        private ICommand _cancelCommand;
        private string _firstName;
        private string _familyName;

        public ICommand SaveEmployeeCommand
        {
            get
            {
                if (_saveEmployeeCommand == null)
                {
                    _saveEmployeeCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
                }
                return _saveEmployeeCommand;
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(GoToLoginView);
                }
                return _cancelCommand;
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string FamilyName
        {
            get { return _familyName; }
            set
            {
                if (value == _familyName) return;
                _familyName = value;
                OnPropertyChanged();
            }
        }


        public CreateEmployeeViewModel(Employee employee)
        :this(employee, new EmployeeRepository()) { }

        public CreateEmployeeViewModel(Employee employee, IEmployeeRepository repository)
        {
            _employee = employee;
            _repository = repository;
        }
        

        private bool CanSaveEmployee(object obj)
        {
            return !(string.IsNullOrWhiteSpace(_firstName) ||
                     string.IsNullOrWhiteSpace(_familyName));
        }

        private void SaveEmployee(object obj)
        {
            AssignPropertiesToModel();
            _repository.Add(_employee);
            _repository.Save();
            GoToCalendar();
        }

        private void AssignPropertiesToModel()
        {
            _employee.FirstName = _firstName;
            _employee.FamilyName = _familyName;
        }

        private void GoToCalendar()
        {
            NavigateTo(new HolidayCalendarViewModel(_employee, _repository));
        }

        private void GoToLoginView(object obj)
        {
            NavigateTo(new LoginViewModel(_repository));
        }
    }
}

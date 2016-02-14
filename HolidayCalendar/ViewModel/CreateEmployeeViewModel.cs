using System;
using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;

namespace HolidayCalendar.ViewModel
{
    public class CreateEmployeeViewModel : UtilityViewModel
    {
        private ICommand _saveEmployeeCommand;
        private ICommand _cancelCommand;
        private string _firstName;
        private string _familyName;

        protected readonly Employee Employee;
        protected readonly IEmployeeRepository Repository;

        public ICommand SaveEmployeeCommand
        {
            get
            {
                if (_saveEmployeeCommand == null)
                {
                    _saveEmployeeCommand = new RelayCommand(SaveEmployeeExecute, SaveEmployeeCanExecute);
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
                    _cancelCommand = new RelayCommand(CancelExecute);
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



        public CreateEmployeeViewModel(Employee employee, MainViewModel mainViewModel)
        :this(employee, new EmployeeRepository(), mainViewModel) { }

        public CreateEmployeeViewModel(Employee employee, IEmployeeRepository repository, MainViewModel mainViewModel)
        :base(mainViewModel)
        {
            Employee = employee;
            Repository = repository;
            Title = "Create Employee";
        }
        

        private bool SaveEmployeeCanExecute(object obj)
        {
            return !(string.IsNullOrWhiteSpace(_firstName) ||
                     string.IsNullOrWhiteSpace(_familyName));
        }

        private void SaveEmployeeExecute(object obj)
        {
            AssignPropertiesToModel();
            Save();
        }

        protected virtual void Save()
        {
            Repository.Add(Employee);
            Repository.Save();
            GoToCalendar();
        }

        private void AssignPropertiesToModel()
        {
            Employee.FirstName = _firstName;
            Employee.FamilyName = _familyName;
        }

        private void GoToCalendar()
        {
            LoadViewModel(new HolidayCalendarViewModel(Employee, Repository));
            Close();
        }

        private void CancelExecute(object obj)
        {
            ChangeUtility(new LoginViewModel(Repository, MainViewModel));
        }
    }
}

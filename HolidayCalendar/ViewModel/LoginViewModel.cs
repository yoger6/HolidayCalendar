using System;
using System.Windows.Controls;
using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class LoginViewModel : ChildViewModel
    {
        private readonly IDirectoryAuthenticator _directoryAuthenticator;

        private ICommand _loginCommand;
        private IEmployeeRepository _employeeRepository;
        private string _domain;
        private string _userName;
        private string _errorMessage;

        public string Domain
        {
            get { return _domain; }
            set
            {
                if (value == _domain) return;
                _domain = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand
        {
            get  
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(LoginExecute);
                }

                return _loginCommand;
            }
        }

        public LoginViewModel()
        :this(new ActiveDirectoryAuthenticator(), new EmployeeRepository()) 
        { }

        public LoginViewModel(IEmployeeRepository repository)
        :this(new ActiveDirectoryAuthenticator(), repository) { }

        public LoginViewModel(IDirectoryAuthenticator authenticator, IEmployeeRepository employeeRepository)
        {
            _directoryAuthenticator = authenticator;
            _employeeRepository = employeeRepository;
        }


        //TODO: there might be need for remember me option, pass too?
        private void LoginExecute(object commandParameter)
        {
            var passwordbox = commandParameter as PasswordBox;
            var password = passwordbox.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Login cannot be executed without password");
             }
        
            if (ValidateCredentials(password))
            {
                _directoryAuthenticator.Domain = Domain;
                _directoryAuthenticator.UserName = UserName;

                if (_directoryAuthenticator.Authenticate(password))
                {
                    GetEmployeeAndLoadNextView();
                }
                else
                {
                    ErrorMessage = "Login failed";
                }
            }
        }

        private void GetEmployeeAndLoadNextView()
        {
            var employee = GetEmployeeForLoggedUser();

            if (employee == null)
            {   var hasher = new Sha256StringHasher();
                var employeeForNewUser = new Employee {Login = hasher.Hash(UserName)};
                NavigateTo(new CreateEmployeeViewModel(employeeForNewUser, _employeeRepository));
            }
            else
            {
                NavigateTo(new HolidayCalendarViewModel(employee, _employeeRepository));
            }
        }

        private Employee GetEmployeeForLoggedUser()
        {
            var hasher = new Sha256StringHasher();
            var existingUser = _employeeRepository.Find(hasher.Hash(UserName));

            return existingUser;
        }

        private bool ValidateCredentials(string password)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(Domain))
            {
                ErrorMessage += "Domain must not be empty \n";
            }
            if (string.IsNullOrEmpty(UserName))
            {
                ErrorMessage += "User name must not be empty \n";
            }
            if (password.Length == 0)
            {
                ErrorMessage += "Password must not be empty";
            }

            return string.IsNullOrEmpty(ErrorMessage);
        }
    }
}

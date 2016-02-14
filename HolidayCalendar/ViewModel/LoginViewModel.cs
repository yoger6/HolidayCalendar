using System;
using System.Windows.Controls;
using System.Windows.Input;
using DataLib.Model;
using DataLib.Repositories;
using HolidayCalendar.Misc;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class LoginViewModel : UtilityViewModel
    {
        private readonly IDirectoryAuthenticator _directoryAuthenticator;

        private ICommand _loginCommand;
        private IEmployeeRepository _employeeRepository;
        private string _domain;
        private string _userName;
        private bool _rememberLogin;

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
        public bool RememberLogin
        {
            get { return _rememberLogin; }
            set
            {
                if (value == _rememberLogin) return;
                _rememberLogin = value;
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

        public LoginViewModel(MainViewModel mainViewModel)
        :this(new DirectoryAuthenticatorDummy(), new EmployeeRepository(SettingsHelper.GetConnectionString()), mainViewModel)
        {
        }

        public LoginViewModel(IEmployeeRepository repository, MainViewModel mainViewModel)
        :this(new ActiveDirectoryAuthenticator(), repository, mainViewModel)
        {
        }

        public LoginViewModel(IDirectoryAuthenticator authenticator, IEmployeeRepository employeeRepository, MainViewModel mainViewModel) 
        : base(mainViewModel)
        {
            Title = "Login";
            _directoryAuthenticator = authenticator;
            _employeeRepository = employeeRepository;
            LoadSettings();
        }

        private void LoadSettings()
        {
            var settings = SettingsHelper.LoadSettings();

            if (settings != null)
            {
                Domain = (string)settings["Domain"];
                UserName = (string)settings["UserName"];
                RememberLogin = (bool) settings["RememberLogin"];
            }
        }

        //TODO: there might be need for remember me option, pass too?
        private void LoginExecute(object commandParameter)
        {
            var passwordbox = commandParameter as PasswordBox;
            if (passwordbox == null)
            {
                throw new ArgumentNullException("Login command requires passwordbox as parameter.");
            }

            var password = passwordbox.Password;
        
            if (ValidateCredentials(password))
            {
                _directoryAuthenticator.Domain = Domain;
                _directoryAuthenticator.UserName = UserName;

                if (_directoryAuthenticator.Authenticate(password))
                {   
                    SettingsHelper.SaveUserSettings(_rememberLogin, _domain, _userName);
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
                ChangeUtility(new CreateEmployeeViewModel(employeeForNewUser, _employeeRepository, MainViewModel));
            }
            else
            {
                LoadViewModel(new HolidayCalendarViewModel(employee, _employeeRepository));
                Close();
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
                AppendErrorMessage("Domain");
            }
            if (string.IsNullOrEmpty(UserName))
            {
                AppendErrorMessage("User name");
            }
            if (password.Length == 0)
            {
                AppendErrorMessage("Password");
            }

            AppendErrorMessageEnd();

            return string.IsNullOrEmpty(ErrorMessage);
        }

    }
}

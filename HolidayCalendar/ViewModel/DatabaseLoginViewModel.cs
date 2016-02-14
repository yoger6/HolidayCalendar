using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Input;
using HolidayCalendar.Misc;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class DatabaseLoginViewModel : UtilityViewModel
    {
        public const int CONNECTION_TIMEOUT = 5;

        private ICommand _loginToDatabaseCommand;

        public DatabaseLoginViewModel(MainViewModel mainViewModel) 
        : base(mainViewModel)
        {
            Title = "Database info";
        }

        public string Source { get; set; }
        public string Catalog { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICommand LoginToDatabaseCommand
        {
            get 
            {
                if (_loginToDatabaseCommand == null)
                {
                    _loginToDatabaseCommand = new RelayCommand(LoginToDatabaseExecute);
                    
                }
             return _loginToDatabaseCommand; 
             }
        }

        private void LoginToDatabaseExecute(object obj)
        {
            var passwordBox = obj as PasswordBox;
            Password = passwordBox?.Password;

            if (ValidateInput())
            {
                var connectionString = GenerateConnectionString();
                if (TestConnection(connectionString))
                {
                    SaveConnection(connectionString);
                    ChangeUtility(new LoginViewModel(MainViewModel));
                }
                else
                {
                    AppendErrorMessage("Couldn't connect to database");
                }
            }
        }

        private bool TestConnection(string connectionString)
        {
            return ConnectionVerifier.IsConnectionValid(connectionString);
        }

        private void SaveConnection(string connectionString)
        {
            SettingsHelper.SaveConnectionString(connectionString);
        }

        private string GenerateConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Source,
                InitialCatalog = Catalog,
                IntegratedSecurity = true,
                ConnectTimeout = CONNECTION_TIMEOUT
            };
            
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                builder.UserID = UserName;
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                builder.Password = Password;
            }

            return builder.ToString();
        }


        private bool ValidateInput()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Source))
            {
                AppendErrorMessage(nameof(Source));
            }
            if (string.IsNullOrWhiteSpace(Catalog))
            {
                AppendErrorMessage(nameof(Catalog));
            }
            AppendErrorMessageEnd();

            return string.IsNullOrWhiteSpace(ErrorMessage);
        }
    }
}

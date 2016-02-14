using HolidayCalendar.Tools;
using HolidayCalendar.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class DbLoginViewModelTest
    {
        private DatabaseLoginViewModel _viewModel;
        private MainViewModel _mainViewModel;
        
        [TestInitialize]
        public void Initialize()
        {
            _mainViewModel = new MainViewModel();
            _viewModel = new DatabaseLoginViewModel(_mainViewModel);
            SetCorrectConnectionString();
            SettingsHelper.ResetSettings();
        }

        [TestMethod]
        public void EmptySourceDisplaysError()
        {
            _viewModel.Source = "";

            _viewModel.LoginToDatabaseCommand.Execute(null);

            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void EmptyUserNameDoesntDisplayError()
        {
            _viewModel.UserName = "";

            _viewModel.LoginToDatabaseCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void EmptyCatalogDisplaysError()
        {
            _viewModel.Catalog = "";

            _viewModel.LoginToDatabaseCommand.Execute(null);

            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void EmptyPasswordDoesntDisplayError()
        {
            _viewModel.Password = "";

            _viewModel.LoginToDatabaseCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void ErrorMessageWhenConnectionFails()
        {
            SetIncorrectConnectionString();

            _viewModel.LoginToDatabaseCommand.Execute(null);

            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void SuccessfulConnectionStoresConnectionStringInSettings()
        {
            SetCorrectConnectionString();

            _viewModel.LoginToDatabaseCommand.Execute(null);

            var connectionString = SettingsHelper.GetConnectionString();
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(connectionString));
        }

        [TestMethod]
        public void SuccessfulConnectionLoadsUserLoginUtility()
        {
            SetCorrectConnectionString();

            _viewModel.LoginToDatabaseCommand.Execute(null);
            
            Assert.IsInstanceOfType(_mainViewModel.CurrentUtilityViewModel, typeof(LoginViewModel));
        }

        private void SetIncorrectConnectionString()
        {
            _viewModel.Source = "source";
            _viewModel.Catalog = "catalog";
            _viewModel.UserName = "user";
            _viewModel.Password = "password";
        }

        private void SetCorrectConnectionString()
        {
            _viewModel.Source = "YOGER-SUPERMAN\\SQLEXPRESS";
            _viewModel.Catalog = "HolidayCalendar";
            _viewModel.UserName = "";
            _viewModel.Password = "";
        }
    }
}

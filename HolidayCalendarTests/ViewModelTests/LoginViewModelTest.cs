using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using DataLib.Model;
using HolidayCalendar;
using HolidayCalendar.Tools;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class LoginViewModelTest
    {
        private DirectoryAuthenticatorStub _authenticatorStub;
        private MainViewModel _mainViewModel;
        private EmployeeRepositoryStub _employeeRepository;
        private List<Employee> _employees; 
        private LoginViewModel _viewModel;
        private PasswordBox _passwordBox;
        private string _userName = "Valid User";
        private string _password = "Valid password";
        private string _domain = "Valid Domain";

        [TestInitialize]
        public void Initialize()
        {
            _authenticatorStub = new DirectoryAuthenticatorStub(_domain, _userName, _password);
            _mainViewModel = new MainViewModel();
            _employees = new List<Employee>();
            _employeeRepository = new EmployeeRepositoryStub(_employees);
            _viewModel = GetNewViewModel();
            _passwordBox = new PasswordBox {Password = _password};
            SettingsHelper.ResetSettings();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CallingLoginWithoutProvidingPasswordAsParameterThrowsException()
        {
            _viewModel.LoginCommand.Execute(null);
        }

        [TestMethod]
        public void SubmitingEmptyUserNameDisplaysError()
        {
            _viewModel.Domain = _domain;
            _viewModel.UserName = string.Empty;

            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }

        [TestMethod]
        public void SubmitingEmptyDomainDisplaysError()
        {
            _viewModel.UserName = _userName;
            _viewModel.Domain = string.Empty;

            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }

        [TestMethod]
        public void SubmitingEmptyPasswordDisplaysError()
        {
            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;
            _passwordBox.Password = "";
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }

        [TestMethod]
        public void SubmitingCorrectCredentialsCausesAuthenticationSuccess()
        {
            var result = false;
            _authenticatorStub.AuthenticationOccured += (sender, b) => result = b;
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SubmitingIncorrectCredentialsCausesAuthenticationFail()
        {
            var result = false;
            _authenticatorStub.AuthenticationOccured += (sender, b) => result = b;
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _passwordBox.Password = "random";
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SubmitingIncorrectCredentialsCausesErroMessageToCreate()
        {
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _passwordBox.Password = "random";
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void SuccessfullAuthenticationButNoEmployeeInDbLoadsEmployeeViewModel()
        {
            _mainViewModel.LoadUtilityViewModel(_viewModel);

            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsInstanceOfType(_mainViewModel.CurrentUtilityViewModel, typeof(CreateEmployeeViewModel));
        }


        //Todo: mainviewModelstub!
        [TestMethod]
        public void SuccessfullAuthenticationEmployeeFoundLoadsCalendarViewModel()
        {
            SettingsHelper.SaveConnectionString("Data Source=YOGER-SUPERMAN\\SQLEXPRESS;Initial Catalog=HolidayCalendar;Integrated Security=True;Connect Timeout=5");
            var hashedUserNameForExistingEmployee = new Sha256StringHasher().Hash(_userName);
            _mainViewModel.LoadUtilityViewModel(_viewModel);
            _employees.Add(new Employee {Login = hashedUserNameForExistingEmployee, FamilyName = "Bar"});

            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;
            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsInstanceOfType(_mainViewModel.CurrentViewModel, typeof(HolidayCalendarViewModel));
        }

        [TestMethod]
        public void LoadsDomainAndLoginIfRememberLoginIsTrue()  //Set at Application Settings
        {
            SettingsHelper.SaveUserSettings(true, "some domain", "some user");
            _viewModel = GetNewViewModel();
            
            Assert.AreEqual(true, _viewModel.RememberLogin);
            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.Domain));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.UserName));
        }

        [TestMethod]
        public void SavesSettingsIfRememberLoginIsTrueAndAuthenticationPositive()
        {
            _viewModel.RememberLogin = true;
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;

            _viewModel.LoginCommand.Execute(_passwordBox);

            Assert.IsNotNull(SettingsHelper.LoadSettings());
        }


        private LoginViewModel GetNewViewModel()
        {
            return new LoginViewModel(_authenticatorStub, _employeeRepository, _mainViewModel);
        }

    }
}

using System;
using System.Collections.Generic;
using DataLib.Model;
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
        private EmployeeRepositoryStub _employeeRepository;
        private List<Employee> _employees; 
        private LoginViewModel _viewModel;
        private string _userName = "Valid User";
        private string _password = "Valid password";
        private string _domain = "Valid Domain";

        [TestInitialize]
        public void Initialize()
        {
            _authenticatorStub = new DirectoryAuthenticatorStub(_domain, _userName, _password);
            _employees = new List<Employee>();
            _employeeRepository = new EmployeeRepositoryStub(_employees);
            _viewModel = new LoginViewModel(_authenticatorStub, _employeeRepository) { MainViewModel = new MainViewModel()};
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

            _viewModel.LoginCommand.Execute(_password);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }
        [TestMethod]
        public void SubmitingEmptyDomainDisplaysError()
        {
            _viewModel.UserName = _userName;
            _viewModel.Domain = string.Empty;

            _viewModel.LoginCommand.Execute(_password);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }

        [TestMethod]
        public void SubmitingEmptyPasswordDisplaysError()
        {
            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;

            _viewModel.LoginCommand.Execute(string.Empty);

            Assert.IsTrue(_viewModel.ErrorMessage.Length > 0);
        }

        [TestMethod]
        public void SubmitingCorrectCredentialsCausesAuthenticationSuccess()
        {
            var result = false;
            _authenticatorStub.AuthenticationOccured += (sender, b) => result = b;
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _viewModel.LoginCommand.Execute(_password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SubmitingIncorrectCredentialsCausesAuthenticationFail()
        {
            var result = false;
            _authenticatorStub.AuthenticationOccured += (sender, b) => result = b;
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _viewModel.LoginCommand.Execute("some random password");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SubmitingIncorrectCredentialsCausesErroMessageToCreate()
        {
            _viewModel.Domain = _domain;
            _viewModel.UserName = _userName;
            _viewModel.LoginCommand.Execute("some random password");

            Assert.IsFalse(string.IsNullOrWhiteSpace(_viewModel.ErrorMessage));
        }

        [TestMethod]
        public void SuccessfullAuthenticationButNoEmployeeInDbLoadsEmployeeViewModel()
        {
            var mainViewModel = new MainViewModel();
            mainViewModel.LoadModel(_viewModel);

            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;
            _viewModel.LoginCommand.Execute(_password);

            Assert.IsInstanceOfType(mainViewModel.CurrentViewModel, typeof(CreateEmployeeViewModel));
        }

        [TestMethod]
        public void SuccessfullAuthenticationEmployeeFoundLoadsCalendarViewModel()
        {
            var hashedUserNameForExistingEmployee = new Sha256StringHasher().Hash(_userName);
            var mainViewModel = new MainViewModel();
            mainViewModel.LoadModel(_viewModel);
            _employees.Add(new Employee {Login = hashedUserNameForExistingEmployee, FamilyName = "Bar"});

            _viewModel.UserName = _userName;
            _viewModel.Domain = _domain;
            _viewModel.LoginCommand.Execute(_password);

            Assert.IsInstanceOfType(mainViewModel.CurrentViewModel, typeof(HolidayCalendarViewModel));
        }
    }
}

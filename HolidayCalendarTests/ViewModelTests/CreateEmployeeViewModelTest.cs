using System;
using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using HolidayCalendar.ViewModel;
using HolidayCalendarTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayCalendarTests.ViewModelTests
{
    [TestClass]
    public class CreateEmployeeViewModelTest
    {
        private MainViewModel _mainViewModel;
        private CreateEmployeeViewModel _viewModel;
        private EmployeeRepositoryStub _repository;
        private List<Employee> _employees;
        private Employee _employee;

        //TODO: test validation
        [TestInitialize]
        public void Initialize()
        {
            _employee = new Employee {Login = "ExistingEmployeesLogin"};
            _employees = new List<Employee> {_employee};
            _repository = new EmployeeRepositoryStub(_employees);
        }

        [TestMethod]
        public void SavingNewEmployeeIsPersistedInRepository()
        {
            var employeeToSave = new Employee {FamilyName = "Smith", FirstName = "John"};
            _viewModel = new CreateEmployeeViewModel(employeeToSave, _repository);
            _viewModel.SaveEmployeeCommand.Execute(null);

            CollectionAssert.Contains(_employees, employeeToSave);
        }
        
        [TestMethod]
        public void SavedEmployeeHasPropertiesFromViewModel()
        {
            _employees.Clear();
            var employeeToSave = new Employee ();
            _viewModel = new CreateEmployeeViewModel(employeeToSave, _repository)
            {
                FirstName = "John",
                FamilyName = "Smith"
            };
            _viewModel.SaveEmployeeCommand.Execute(null);

            Assert.AreEqual(_viewModel.FirstName, _employees.First().FirstName);
            Assert.AreEqual(_viewModel.FamilyName, _employees.First().FamilyName);
        }

        [TestMethod]
        public void SaveCannotBeExecutedIfAnyNameInViewModelIsNullOrEmpty()
        {
            var employeeToSave = new Employee();
            _viewModel = new CreateEmployeeViewModel(employeeToSave, _repository)
            {
                FirstName = null,
                FamilyName = "Smith"
            };
            var canSave = _viewModel.SaveEmployeeCommand.CanExecute(null);

            Assert.IsFalse(canSave);
        }

        [TestMethod]
        public void CancelCommandNavigatesToLoginScreen()
        {
            _viewModel = new CreateEmployeeViewModel(new Employee(), _repository);
            _mainViewModel = new MainViewModel();
            _mainViewModel.LoadModel(_viewModel);

            _viewModel.CancelCommand.Execute(null);

            Assert.IsInstanceOfType(_mainViewModel.CurrentViewModel, typeof(LoginViewModel));
        }

        [TestMethod]
        public void SaveCommandNavigatesToCalendarIfSuccessfullySaved()
        {
            _employees.Clear();
            _viewModel = new CreateEmployeeViewModel(new Employee (), _repository)
            {
                FirstName = "Adam", 
                FamilyName = "West" 
            
            };
            _mainViewModel = new MainViewModel();
            _mainViewModel.LoadModel(_viewModel);

            _viewModel.SaveEmployeeCommand.Execute(null);

            Assert.IsInstanceOfType(_mainViewModel.CurrentViewModel, typeof(HolidayCalendarViewModel));
        }
    }
}

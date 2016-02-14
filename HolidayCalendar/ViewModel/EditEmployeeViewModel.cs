using System;
using DataLib.Model;
using DataLib.Repositories;

namespace HolidayCalendar.ViewModel
{
    public class EditEmployeeViewModel : CreateEmployeeViewModel
    {
        public event EventHandler ViewModelClosed;

        public EditEmployeeViewModel(Employee employee, MainViewModel mainViewModel) 
        : this(employee, new EmployeeRepository(), mainViewModel)
        {
        }

        public EditEmployeeViewModel(Employee employee, IEmployeeRepository repository, MainViewModel mainViewModel) 
        : base(employee, repository, mainViewModel)
        {
            DisplayEmployeeProperties(employee);
        }

        private void DisplayEmployeeProperties(Employee employee)
        {
            FirstName = employee.FirstName;
            FamilyName = employee.FamilyName;
            Title = "Edit employee";
        }

        protected override void Save()
        {
            Repository.Update(Employee);
            Repository.Save();
            Close();
        }

        protected override void Close()
        {
            OnViewModelClosed();
            base.Close();
        }

        protected virtual void OnViewModelClosed()
        {
            ViewModelClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
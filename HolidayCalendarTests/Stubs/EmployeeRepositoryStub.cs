using System.Collections.Generic;
using System.Linq;
using DataLib.Model;
using DataLib.Repositories;

namespace HolidayCalendarTests.Stubs
{
    public class EmployeeRepositoryStub : RepositoryStub<Employee>, IEmployeeRepository
    {
        public EmployeeRepositoryStub()
            :this(new List<Employee>()) { }

        public EmployeeRepositoryStub(List<Employee> users)
            :base(users) { }

        public Employee Find(string login)
        {
            return Items.FirstOrDefault(x => x.Login == login);
        }
    }
}
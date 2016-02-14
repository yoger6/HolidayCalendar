using System;
using System.Collections.Generic;
using System.Linq;
using DataLib.Model;

namespace DataLib.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository() { }

        public EmployeeRepository(string connectionString)
        :base(connectionString) 
        { }

        public override List<Employee> GetAll()
        {
            return DbSet.Include("Holidays").ToList();
        }
        
        
        public Employee Find(string login)
        {
            return DbSet.FirstOrDefault(x=>x.Login == login);
        }

        public void AttachHolidays(IEnumerable<HolidayReason> reasons)
        {
            foreach (var holidayReason in reasons)
            {
                Context.HolidayReasons.Attach(holidayReason);
            }
        }
    }
}

using System.Collections.Generic;
using DataLib.Model;

namespace DataLib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee Find(string login);
        void AttachHolidays(IEnumerable<HolidayReason> reasons);
    }
}

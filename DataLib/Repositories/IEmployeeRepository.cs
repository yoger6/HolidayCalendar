using DataLib.Model;

namespace DataLib.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee Find(string login);
    }
}

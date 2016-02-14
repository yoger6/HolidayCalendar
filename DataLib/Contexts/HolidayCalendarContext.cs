using System.Data.Entity;
using DataLib.Mapping;
using DataLib.Model;

namespace DataLib.Contexts
{
    public class HolidayCalendarContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HolidayReason> HolidayReasons { get; set; }
        public DbSet<Day> Days { get; set; }


        public HolidayCalendarContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new HolidayCalendarDatbaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMapping());
            modelBuilder.Configurations.Add(new HolidayReasonMapping());
            modelBuilder.Configurations.Add(new DayMapping());
        }
    }
}

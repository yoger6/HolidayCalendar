using System.Data.Entity;
using DataLib.Mapping;
using DataLib.Model;

namespace DataLib.Contexts
{
    public class HolidyCalendarContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<HolidayReason> HolidayReasons { get; set; }


        public HolidyCalendarContext(string conntectionString)
            : base(conntectionString)
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

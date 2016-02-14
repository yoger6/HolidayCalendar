using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataLib.Model;

namespace DataLib.Mapping
{
    public class DayMapping : EntityTypeConfiguration<Day>
    {
        public DayMapping()
        {
            HasKey(x => new {x.Id, x.EmployeeId});
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.HolidayReason);
        }
    }
}

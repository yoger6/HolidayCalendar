using System.Data.Entity.ModelConfiguration;
using DataLib.Model;

namespace DataLib.Mapping
{
    public class DayMapping : EntityTypeConfiguration<Day>
    {
        public DayMapping()
        {
            HasKey(x => x.Id);
            HasRequired(x => x.HolidayReason);
        }
    }
}

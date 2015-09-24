using System.Data.Entity.ModelConfiguration;
using DataLib.Model;

namespace DataLib.Mapping
{
    public class HolidayReasonMapping : EntityTypeConfiguration<HolidayReason>
    {
        public HolidayReasonMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(20);
            Property(x => x.ShortName)
                .IsRequired()
                .HasMaxLength(3);
            Property(x => x.Color.A)
                .IsRequired();
            Property(x => x.Color.R)
                .IsRequired();
            Property(x => x.Color.G)
                .IsRequired();
            Property(x => x.Color.B)
                .IsRequired();
            ToTable("HolidayReasons");
        }
    }
}

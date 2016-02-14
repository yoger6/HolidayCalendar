using System.Data.Entity.ModelConfiguration;
using DataLib.Model;

namespace DataLib.Mapping
{
    public class EmployeeMapping : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            Property(x => x.FamilyName)
                .IsRequired()   
                .HasMaxLength(30);
            Property(x => x.Login)
                .IsRequired()
                .HasMaxLength(64);
            HasMany(x => x.Holidays).WithRequired()
                                    .HasForeignKey(x=>x.EmployeeId)
                                    .WillCascadeOnDelete();
            ToTable("Employees");
        }
    }
}

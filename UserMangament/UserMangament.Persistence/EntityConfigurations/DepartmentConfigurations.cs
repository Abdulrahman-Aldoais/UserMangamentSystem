using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(e => e.Emp_Holidays_Orders).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.Employees).WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.Jobs).WithOne().OnDelete(DeleteBehavior.Restrict);

        }
    }
}

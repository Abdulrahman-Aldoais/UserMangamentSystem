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


            builder.HasData(
               new Department
               {
                   Id = 1,
                   Name = "قسم الاتي",
                   CreatedDate = DateTime.Now,
                   CreatedBy = null,
                   DeletedBy = null,
                   IsDeleted = false,
                   ModifiedBy = null,
                   ModifiedDate = null,

               },
                new Department
                {
                    Id = 2,
                    Name = "  قسم موارد بشرية",
                    CreatedDate = DateTime.Now,
                    CreatedBy = null,
                    DeletedBy = null,
                    IsDeleted = false,
                    ModifiedBy = null,
                    ModifiedDate = null,
                }
               );

        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class Emp_Holidays_OrderConfigurations : IEntityTypeConfiguration<Emp_Holidays_Order>
    {
        public void Configure(EntityTypeBuilder<Emp_Holidays_Order> builder)
        {

            builder.HasOne(eho => eho.Department)
           .WithMany(d => d.Emp_Holidays_Orders)
           .HasForeignKey(eho => eho.DepartmentId)
           .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(eho => eho.Employee)
                   .WithMany()
                   .HasForeignKey(eho => eho.EmployeeId);

            builder.HasOne(eho => eho.User)
                  .WithMany()
                  .HasForeignKey(eho => eho.UserId);


            builder.HasOne(eho => eho.Employee)
                   .WithMany(e => e.Emp_Holidays_Orders)
                   .HasForeignKey(eho => eho.EmployeeId) // Explicitly specifying the foreign key
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

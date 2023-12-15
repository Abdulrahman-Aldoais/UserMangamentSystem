using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(d => d.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(h => h.WorkingHour);
            builder.HasMany(e => e.Emp_Holidays_Orders).WithOne(e => e.Employee).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.Attendances).WithOne(e => e.Employee).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Department)
        .WithMany(d => d.Employees)
        .HasForeignKey(e => e.DepartmentId) // Explicitly specifying the foreign key
        .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(j => j.Job)
    .WithMany(job => job.Employees)
    .HasForeignKey(e => e.JobId) // Explicitly specifying the foreign key for Employee's Job
    .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

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
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(j => j.Job)
                    .WithMany(job => job.Employees)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                new Employee
                {
                    Id = 1,
                    Name = "عبدالرحمن علي سرحان الدعيس",
                    CreatedBy = null,
                    Phone = "775115810",
                    JobId = 1,
                    JobTitle = "مطور انظمة",
                    JobDescription = "",
                    Salary = 700000,
                    WorkingHourId = 3,
                    CreatedDate = DateTime.Now,
                    DeletedBy = null,
                    ModifiedBy = null,
                    DepartmentId = 1,
                    IsActive = true,
                    HireDate = DateTime.Now,
                    IsDeleted = false,
                    ModifiedDate = null,
                },
                 new Employee
                 {
                     Id = 2,
                     Name = "امين حميد اليعري",
                     CreatedBy = null,
                     Phone = "775115810",
                     CreatedDate = DateTime.Now,
                     DeletedBy = null,
                     ModifiedBy = null,
                     DepartmentId = 1,
                     IsActive = true,
                     HireDate = DateTime.Now,
                     IsDeleted = false,
                     JobId = 1,
                     JobTitle = "مطور انظمة",
                     JobDescription = "",
                     Salary = 700000,
                     WorkingHourId = 3,
                     ModifiedDate = null,
                 }
                );


        }
    }
}

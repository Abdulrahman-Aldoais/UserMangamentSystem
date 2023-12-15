using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {


            builder.ToTable("Attendance");



            builder.HasOne(u => u.User);
            builder.HasOne(e => e.Employee)
                .WithMany(emp => emp.Attendances)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

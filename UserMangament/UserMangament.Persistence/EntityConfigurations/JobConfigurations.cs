using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class JobConfigurations : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100).IsSparse(false);

            builder.HasOne(j => j.Department)
                   .WithMany(d => d.Jobs)
                   .HasForeignKey(j => j.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(j => j.Employees)
    .WithOne(e => e.Job)
    .HasForeignKey(e => e.JobId)
    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

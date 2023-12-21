using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> builder)
        {
            builder.HasData(
              new WorkingHour
              {
                  Id = 1,
                  Hours = 8,
                  Name = " ",
                  Employment_TypeId = 1,
              },
               new WorkingHour
               {
                   Id = 2,
                   Hours = 6,
                   Name = " ",
                   Employment_TypeId = 2,
               },
                new WorkingHour
                {
                    Id = 3,
                    Hours = 4,
                    Name = " ",
                    Employment_TypeId = 3,

                }
              );
        }
    }
}

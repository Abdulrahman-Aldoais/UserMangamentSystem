using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class EmploymentTypeConfiguration : IEntityTypeConfiguration<Employment_Type>
    {
        public void Configure(EntityTypeBuilder<Employment_Type> builder)
        {
            builder.HasData(
                  new Employment_Type
                  {
                      Id = 1,
                      Name = "كلي",
                      Description = "دوام الموضفين",

                  },
                   new Employment_Type
                   {
                       Id = 2,
                       Name = "جزئي",
                       Description = "دوام الموضفين",
                   },
                    new Employment_Type
                    {
                        Id = 3,
                        Name = "ساعات",
                        Description = "دوام الموضفين",


                    }
                  );
        }
    }
}

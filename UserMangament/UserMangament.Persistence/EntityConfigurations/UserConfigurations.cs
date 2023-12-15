using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserMangament.Persistence.EntityConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasData(
     new User
     {
         Id = 1,
         Name = "عبدالرحمن الدعيس",
         UserName = "admin",
         Email = "abdulrahman@admin.com",
         Phone = "775115810",
         Age = 23,
         IsActive = true,
         CreatedBy = null,
         ModifiedBy = null,
         DeletedBy = null,
         IsDeleted = false,
         CreatedDate = DateTime.Now,
         ModifiedDate = null,
     });

        }
    }
}

//        public static async Task SeedAsync(UserManager<User> _userManager)
//        {
//            var usersCount = await _userManager.Users.CountAsync();
//            if (usersCount <= 0)
//            {
//                var defaultuser = new User()
//                {
//                    UserName = "admin",
//                    Email = "abdulrahman@admin.com",
//                    PhoneNumber = "775115810",
//                    EmailConfirmed = true,
//                    PhoneNumberConfirmed = true
//                };
//                await _userManager.CreateAsync(defaultuser, "@Password");
//                await _userManager.AddToRoleAsync(defaultuser, "Admin");
//            }
//        }
//    }
//}

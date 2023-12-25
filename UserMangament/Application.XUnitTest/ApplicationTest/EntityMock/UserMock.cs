using Application.Features.Users.Dtos.Get;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.XUnitTest.ApplicationTest.EntityMock
{
    public class UserMock
    {
        public List<GetUserOutput> users;


        public UserMock()
        {
            users = new List<GetUserOutput>
       {
      new GetUserOutput
       {
        Id = 1,
        UserName = "abdulrahman",
        Age = 30,
        Phone = "123-456-7890",
        Email = "abdulrahman@exe.com",
        IsActive = true,
        AccountCancellationStatusBy = 1,
        CreatedDate =  DateTime.Now,
        ModifiedDate = DateTime.Now,
        CreatedBy = 1,
        ModifiedBy = 1,
    },
      new GetUserOutput
    {
        Id = 2,
        UserName = "ameen",
        Age = 25,
        Phone = "098-765-4321",
        Email = "ameen@exe.com",
        IsActive = true,
        AccountCancellationStatusBy = 1,
        CreatedDate =  DateTime.Now,
        ModifiedDate = DateTime.Now,
        CreatedBy = 1,
        ModifiedBy = 1,
    }
                };

        }

      public  User user = new User()
        {
            Id = 2,
            UserName = "ameen",
            Age = 25,
            Phone = "098-765-4321",
            Email = "ameen@exe.com",
            IsActive = true,
            AccountCancellationStatusBy = 1,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedBy = 1,
            ModifiedBy = 1,
        };
    }
}

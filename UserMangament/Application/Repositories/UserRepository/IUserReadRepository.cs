using Application.Features.Users.Dtos.Get;
using Domain.Entities;
using Domain.Repositories.Interface;
using System.Linq.Expressions;

namespace Application.Repositories.UserRepository
{
    public interface IUserReadRepository : IReadRepository<User>
    {
       
    }
}

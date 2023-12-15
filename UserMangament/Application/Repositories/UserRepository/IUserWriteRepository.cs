using Core.Persistence.Repositories.Interface;
using Domain.Entities;

namespace Application.Repositories.UserRepository
{
    public interface IUserWriteRepository : IWriteRepository<User>
    {
    }
}

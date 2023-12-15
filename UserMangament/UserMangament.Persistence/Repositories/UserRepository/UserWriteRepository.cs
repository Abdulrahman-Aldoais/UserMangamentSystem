using Application.Repositories.UserRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.UserRepository
{
    public class UserWriteRepository : WriteRepository<User, ApplicationDBContext>, IUserWriteRepository
    {
        public UserWriteRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}


using Application.Repositories.UserRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.UserRepository
{
    public class UserReadRepository : ReadRepository<User, ApplicationDBContext>, IUserReadRepository
    {
        public UserReadRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
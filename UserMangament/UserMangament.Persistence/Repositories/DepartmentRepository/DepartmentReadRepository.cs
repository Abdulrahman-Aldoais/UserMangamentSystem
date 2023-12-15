using Application.Repositories.DepartmentRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.DepartmentRepository
{
    public class DepartmentReadRepository : ReadRepository<Department, ApplicationDBContext>, IDepartmentReadRepository
    {

        public DepartmentReadRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

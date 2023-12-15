using Application.Repositories.DepartmentRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.DepartmentRepository
{
    public class DepartmentWriteRepository : WriteRepository<Department, ApplicationDBContext>, IDepartmentWriteRepository
    {
        public DepartmentWriteRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

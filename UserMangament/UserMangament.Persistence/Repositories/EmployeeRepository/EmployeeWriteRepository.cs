using Application.Repositories.EmployeeRepositoty;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.EmployeeRepository
{
    public class EmployeeWriteRepository : WriteRepository<Employee, ApplicationDBContext>, IEmployeeWriteRepositoty
    {
        public EmployeeWriteRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

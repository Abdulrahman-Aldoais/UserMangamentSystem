using Application.Repositories.EmployeeRepositoty;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.EmployeeRepository
{
    public class EmployeeReadRepository : ReadRepository<Employee, ApplicationDBContext>, IEmployeeReadRepositoty
    {
        public EmployeeReadRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

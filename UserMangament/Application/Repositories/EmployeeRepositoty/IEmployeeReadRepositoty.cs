using Domain.Entities;
using Domain.Repositories.Interface;

namespace Application.Repositories.EmployeeRepositoty
{
    public interface IEmployeeReadRepositoty : IReadRepository<Employee>
    {
    }
}

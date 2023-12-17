using Core.Persistence.Repositories.Interface;
using Domain.Entities;

namespace Application.Repositories.EmployeeRepositoty
{
    public interface IEmployeeWriteRepositoty : IWriteRepository<Employee>
    {
    }
}

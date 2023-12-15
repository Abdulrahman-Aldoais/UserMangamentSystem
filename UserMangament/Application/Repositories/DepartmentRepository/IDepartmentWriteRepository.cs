using Core.Persistence.Repositories.Interface;
using Domain.Entities;

namespace Application.Repositories.DepartmentRepository
{
    public interface IDepartmentWriteRepository : IWriteRepository<Department>
    {
    }
}

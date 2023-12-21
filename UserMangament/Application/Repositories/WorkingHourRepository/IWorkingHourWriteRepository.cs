using Core.Persistence.Repositories.Interface;
using Domain.Entities;

namespace Application.Repositories.WorkingHourRepository
{
    public interface IWorkingHourWriteRepository : IWriteRepository<WorkingHour>
    {
    }
}

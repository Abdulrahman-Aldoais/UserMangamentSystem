using Application.Repositories.WorkingHourRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.WorkingHoursRepository
{
    public class WorkingHourWriteRepository : WriteRepository<WorkingHour, ApplicationDBContext>, IWorkingHourWriteRepository
    {
        public WorkingHourWriteRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

using Application.Repositories.WorkingHourRepository;
using Domain.Entities;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.WorkingHoursRepository
{
    public class WorkingHourReadRepository : ReadRepository<WorkingHour, ApplicationDBContext>, IWorkingHourReadRepository
    {
        public WorkingHourReadRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

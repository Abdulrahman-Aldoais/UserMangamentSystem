using Domain.Entities;
using Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.JobsRepositorys
{
    public interface IJobsReadRepository : IReadRepository<Job>
    {
    }
}

﻿using Application.Repositories.JobsRepositorys;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;

namespace UserMangament.Persistence.Repositories.JobRepositoty
{
    public class JobWriteRepository :WriteRepository<Job,ApplicationDBContext>,IJobsWriteRepository
    {
        public JobWriteRepository(ApplicationDBContext context):base(context)
        {
            
        }
    }
}

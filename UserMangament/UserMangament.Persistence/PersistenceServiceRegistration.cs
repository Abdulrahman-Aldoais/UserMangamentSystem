using Application.Repositories.DepartmentRepository;
using Application.Repositories.EmployeeRepositoty;
using Application.Repositories.JobsRepositorys;
using Application.Repositories.UserRepository;
using Application.Repositories.WorkingHourRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.DepartmentRepository;
using UserMangament.Persistence.Repositories.EmployeeRepository;
using UserMangament.Persistence.Repositories.JobRepositoty;
using UserMangament.Persistence.Repositories.UserRepository;
using UserMangament.Persistence.Repositories.WorkingHoursRepository;

namespace UserMangament.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            {
                services.AddDbContext<ApplicationDBContext>(
                opt =>
                {
                    opt.UseSqlServer(ConnectionStrings.localString);
                    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                // Repositories


                services.AddScoped<IUserReadRepository, UserReadRepository>();
                services.AddScoped<IUserWriteRepository, UserWriteRepository>();
                services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
                services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
                services.AddScoped<IEmployeeReadRepositoty, EmployeeReadRepository>();
                services.AddScoped<IEmployeeWriteRepositoty, EmployeeWriteRepository>();
                services.AddScoped<IJobsReadRepository, JobReadRepository>();
                services.AddScoped<IJobsWriteRepository, JobWriteRepository>();

                services.AddScoped<IWorkingHourReadRepository, WorkingHourReadRepository>();
                services.AddScoped<IWorkingHourWriteRepository, WorkingHourWriteRepository>();



                return services;
            }
        }
    }
}

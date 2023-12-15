using Application.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.UserRepository;

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


                return services;
            }
        }
    }
}

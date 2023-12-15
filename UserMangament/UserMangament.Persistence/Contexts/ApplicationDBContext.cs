
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace UserMangament.Persistence.Contexts
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {


        }
        public DbSet<User> User { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<DepartmentEmployeeCount> DepartmentEmployeeCounts { get; set; }
        public DbSet<Emp_Holidays_Order> Emp_Holidays_Orders { get; set; }
        public DbSet<Emp_Holidays_Stock> Emp_Holidays_Stocks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employment_Type> Employment_Types { get; set; }
        public DbSet<HolidaysType> HolidaysTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }


        protected IConfiguration Configuration { get; set; }

        public ApplicationDBContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

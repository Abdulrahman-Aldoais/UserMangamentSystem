using Application.Repositories.EmployeeRepositoty;
using Application.Repositories.UserRepository;
using Domain.Entities;

namespace Application.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeWriteRepositoty _employeeWriteRepositoty;
        public EmployeeService(IEmployeeWriteRepositoty employeeWriteRepositoty)
        {
            _employeeWriteRepositoty = employeeWriteRepositoty;
        }
        public async Task<string> AddNewEmployee(Employee employee)
        {
            try
            {
                await _employeeWriteRepositoty.AddAsync(employee);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
                //throw ex;
            }
        }
    }
}

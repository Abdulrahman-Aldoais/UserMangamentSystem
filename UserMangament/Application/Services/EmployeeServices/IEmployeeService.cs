using Domain.Entities;

namespace Application.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<string> AddNewEmployee(Employee employee);
    }
}

using Application.Repositories.EmployeeRepositoty;
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
            catch (Exception ex)
            {

                // Log the exception details, including inner exception
                Console.WriteLine("Exception message: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);

                // Check for inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception message: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Exception stack trace: " + ex.InnerException.StackTrace);
                    // Log any additional information from


                }
                return "Failed";
            }
        }
    }
}

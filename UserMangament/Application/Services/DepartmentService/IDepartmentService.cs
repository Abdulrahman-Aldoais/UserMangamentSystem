using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using Application.Features.Users.Dtos.Get;
using Domain.Entities;

namespace Application.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<List<GetListDepartmentOutput>> GetAllDepartmentAsync(GetListDepartmentOutput department);
        Task<GetDepartmentOutput> GetDepartmentByIdAsync(int departmentId);
        Task<string> AddNewDepartment(Department department);
        Task<string> UpdateDepartment(Department department);
    }
}

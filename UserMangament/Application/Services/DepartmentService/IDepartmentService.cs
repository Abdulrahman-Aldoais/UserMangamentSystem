using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;

namespace Application.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<List<GetListDepartmentOutput>> GetAllDepartmentAsync(GetListDepartmentOutput department);
        Task<GetDepartmentOutput> GetDepartmentByIdAsync(int departmentId);
    }
}

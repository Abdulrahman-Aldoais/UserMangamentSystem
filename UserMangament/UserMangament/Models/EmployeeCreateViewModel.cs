using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using Application.Features.Employees.Dtos.Get;

namespace UserMangament.Models
{
    public class EmployeeCreateViewModel
    {
        public GetEmployeeOutput employeeOutput { get; set; }
        public List<GetListDepartmentOutput> departmentOutput { get; set; }
    }
}

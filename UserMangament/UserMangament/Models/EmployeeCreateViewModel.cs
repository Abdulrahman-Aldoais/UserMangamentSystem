using Application.Features.Departments.Dtos.GetList;
using Application.Features.Employees.Dtos.Get;
using Application.Features.WorkingHours.Dtos.GetList;

namespace UserMangament.Models
{
    public class EmployeeCreateViewModel
    {
        public GetEmployeeOutput employeeOutput { get; set; }
        public List<GetListDepartmentOutput> departmentListOutput { get; set; }
        public List<GetListWorkingHourOutput> workinHourListOutput { get; set; }
    }
}

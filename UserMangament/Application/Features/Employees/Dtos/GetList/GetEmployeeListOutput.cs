using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Dtos.GetList
{
    public class GetEmployeeListOutput
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public double Salary { get; set; }
        public int WorkingHourId { get; set; }
    }
}

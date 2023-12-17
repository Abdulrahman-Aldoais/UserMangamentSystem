using Application.Features.Employees.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployesCommand : IRequest<BaseCommandResponse<GetEmployeeOutput>>
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int AccountCancellationStatusBy { get; set; }
        public int CreateBy { get; set; }
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

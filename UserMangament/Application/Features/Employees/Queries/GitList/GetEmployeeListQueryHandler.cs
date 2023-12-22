using Application.Features.Employees.Dtos.GetList;
using Application.Repositories.EmployeeRepositoty;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employees.Queries.GitList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, BaseCommandResponse<List<GetEmployeeListOutput>>>
    {
        private readonly IEmployeeReadRepositoty _employeeReadRepositoty;
        private readonly IMapper _mapper;
        public GetEmployeeListQueryHandler(IEmployeeReadRepositoty employeeReadRepositoty, IMapper mapper)
        {
            _employeeReadRepositoty = employeeReadRepositoty;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<List<GetEmployeeListOutput>>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var respons = new BaseCommandResponse<List<GetEmployeeListOutput>>();

            var result = _employeeReadRepositoty.GetAll().Include(x => x.Department);
            var getAllEmpolyee = result.Select(x => new GetEmployeeListOutput
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                DepartmentName = x.Department.Name,
                HireDate = x.HireDate.ToShortDateString(),
                JobTitle = x.JobTitle,
                Salary = x.Salary,
                WorkingHour = x.WorkingHour.Hours,
            });
            var mappingResult = _mapper.Map<List<GetEmployeeListOutput>>(getAllEmpolyee);

            if (!getAllEmpolyee.Any())
            {
                respons.Success = false;
                respons.StatusCode = System.Net.HttpStatusCode.BadRequest;
                respons.Data = new List<GetEmployeeListOutput>();
                respons.Errors = null;
                respons.Message = SharedResourcesKeys.BadRequest;


            }
            else
            {
                respons.Data = mappingResult;
                respons.StatusCode = System.Net.HttpStatusCode.OK;
                respons.Message = SharedResourcesKeys.Success;
                respons.Success = true;
                respons.Errors = null;


            }

            return respons;
        }
    }
}

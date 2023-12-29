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

            var result = await _employeeReadRepositoty.GetAll()
                                         .Include(x => x.Department)
                                         .Include(h => h.WorkingHour)
                                         .ToListAsync();

            var allEmplyees = result.Select(x => new GetEmployeeListOutput
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                DepartmentName = x.Department.Name,
                HireDate = x.HireDate != default ? x.HireDate.ToShortDateString() : null,
                JobTitle = x.JobTitle,
                Salary = x.Salary,
                WorkingHour = x.WorkingHour.Hours,
            }).ToList();


            var empMapp = _mapper.Map<List<GetEmployeeListOutput>>(allEmplyees);

            if (result == null)
            {
                respons.Success = false;
                respons.StatusCode = System.Net.HttpStatusCode.BadRequest;
                respons.Data = null;
                respons.Errors = null;
                respons.Message = SharedResourcesKeys.BadRequest;


            }
            else
            {
                respons.Success = true;
                respons.Data = empMapp;
                respons.StatusCode = System.Net.HttpStatusCode.OK;
                respons.Message = SharedResourcesKeys.Success;
                respons.Errors = null;


            }

            return respons;
        }
    }
}

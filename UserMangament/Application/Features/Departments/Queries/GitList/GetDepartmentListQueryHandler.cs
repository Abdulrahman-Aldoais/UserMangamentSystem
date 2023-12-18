using Application.Features.Departments.Dtos.GetList;
using Application.Repositories.DepartmentRepository;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Queries.GitList
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, BaseCommandResponse<List<GetListDepartmentOutput>>>
    {
        private readonly IDepartmentReadRepository _readRepository;
        private readonly IMapper _mapper;
        public GetDepartmentListQueryHandler(IDepartmentReadRepository departmentReadRepository, IMapper mapper)
        {
            _readRepository = departmentReadRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<List<GetListDepartmentOutput>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<List<GetListDepartmentOutput>>();

            var result = await _readRepository.GetListAsync();
            if (!result.Any())
            {
                response.Success = false;
                response.Message = SharedResourcesKeys.BadRequest;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Errors = null;
                response.Data = new List<GetListDepartmentOutput>();
            }
            else
            {
                var resultMapp = _mapper.Map<List<GetListDepartmentOutput>>(result);
                response.Data = resultMapp;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
            }
            return response;
        }
    }
}

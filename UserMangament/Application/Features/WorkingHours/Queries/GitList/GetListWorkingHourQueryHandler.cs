using Application.Features.WorkingHours.Dtos.GetList;
using Application.Repositories.WorkingHourRepository;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.WorkingHours.Queries.GitList
{
    public class GetListWorkingHourQueryHandler : IRequestHandler<GetListWorkingHourQuery, BaseCommandResponse<List<GetListWorkingHourOutput>>>
    {
        private readonly IWorkingHourReadRepository _workingHourReadRepository;
        private readonly IMapper _mapper;
        public GetListWorkingHourQueryHandler(IWorkingHourReadRepository workingHourReadRepository, IMapper mapper)
        {
            _workingHourReadRepository = workingHourReadRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<List<GetListWorkingHourOutput>>> Handle(GetListWorkingHourQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<List<GetListWorkingHourOutput>>();

            var result = await _workingHourReadRepository.GetListAsync();
            if (!result.Any())
            {
                response.Success = false;
                response.Message = SharedResourcesKeys.BadRequest;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Errors = null;
                response.Data = new List<GetListWorkingHourOutput>();
            }
            else
            {
                var resultMapp = _mapper.Map<List<GetListWorkingHourOutput>>(result);
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

using Application.Features.WorkingHours.Dtos.GetList;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.WorkingHours.Queries.GitList
{
    public class GetListWorkingHourQuery : IRequest<BaseCommandResponse<List<GetListWorkingHourOutput>>>
    {
    }
}

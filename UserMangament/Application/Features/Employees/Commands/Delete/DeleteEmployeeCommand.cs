using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand:IRequest<BaseCommandResponse<int>>
    {
        public int Id { get; set; }
    }
}

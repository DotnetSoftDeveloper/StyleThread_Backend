using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.GenericCommands
{
    public record GenericDeleteByIdCommand<TId, TResponse>(TId Id) : IRequest<TResponse> where TResponse : class;
}

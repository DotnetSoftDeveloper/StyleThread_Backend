using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.GenericCommands
{
    public record class GenericDeleteCommand<TRequest, TResponse>(TRequest Entity) : IRequest<TResponse> where TRequest : class where TResponse : class;
}

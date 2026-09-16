using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var result = await Task.WhenAll(validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));
                var failers = result.SelectMany(x => x.Errors).Where(f => f != null).ToList();
                if (failers.Count > 0)
                    throw new ValidationException(failers);
            }
            var response = await next();
            return response;
        }
    }
}

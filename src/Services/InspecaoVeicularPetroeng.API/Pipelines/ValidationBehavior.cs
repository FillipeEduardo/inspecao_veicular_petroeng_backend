using System.Net;
using FluentValidation;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Mediator.Interfaces;

namespace InspecaoVeicularPetroeng.API.Pipelines;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handler(TRequest request, CancellationToken cancellationToken,
        Func<Task<TResponse>> next)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                return (TResponse)(object)new ErrorResult(failures.Select(x => x.ErrorMessage).ToList(),
                    HttpStatusCode.BadRequest);
        }

        var result = await next();
        return result;
    }
}
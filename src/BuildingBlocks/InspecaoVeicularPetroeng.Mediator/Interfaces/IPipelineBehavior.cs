namespace InspecaoVeicularPetroeng.Mediator.Interfaces;

public interface IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handler(
        TRequest request,
        CancellationToken cancellationToken,
        Func<Task<TResponse>> next);
}
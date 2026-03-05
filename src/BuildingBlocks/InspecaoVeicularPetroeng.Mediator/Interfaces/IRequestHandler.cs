namespace InspecaoVeicularPetroeng.Mediator.Interfaces;

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handler(TRequest request, CancellationToken cancellationToken);
}
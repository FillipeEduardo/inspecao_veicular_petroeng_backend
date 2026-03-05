using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InspecaoVeicularPetroeng.Mediator.Implementation;

public class Mediator(IServiceProvider provider) : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = provider.GetService(handlerType);

        if (handler is null)
            throw new InvalidOperationException($"O Handler {request.GetType().Name} não foi encontrado.");

        var method = handlerType.GetMethod("Handler")!;

        var handlerDelegate = () =>
            (Task<TResponse>)method.Invoke(handler, [request, CancellationToken.None])!;

        var behaviorType = typeof(IPipelineBehavior<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        var behaviors = provider
            .GetServices(behaviorType)
            .Cast<object>()
            .Reverse()
            .ToList();

        var pipeline = handlerDelegate;

        foreach (var behavior in behaviors)
        {
            var b = behavior;

            var behaviorHandle = behaviorType.GetMethod("Handler")!;

            var nextCopy = pipeline;
            pipeline = () => (Task<TResponse>)behaviorHandle.Invoke(
                b,
                [request, CancellationToken.None, nextCopy])!;
        }

        return await pipeline();
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        var handlers = provider.GetServices(handlerType);

        foreach (var handler in handlers)
            await (Task)handlerType.GetMethod("Handler")!.Invoke(handler, [notification, cancellationToken])!;
    }
}
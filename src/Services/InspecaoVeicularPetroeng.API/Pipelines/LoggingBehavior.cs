using System.Diagnostics;
using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Serilog;

namespace InspecaoVeicularPetroeng.API.Pipelines;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handler(TRequest request, CancellationToken cancellationToken,
        Func<Task<TResponse>> next)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();
        try
        {
            Log.Information("Manipulando {RequestName} com {@Request}", requestName, request);
            var result = await next();
            stopwatch.Stop();
            Log.Information("Manipulando {RequestName} em {ElapsedMilliseconds}ms com {@Response}",
                requestName,
                stopwatch.ElapsedMilliseconds,
                result);

            return result;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Log.Error(ex, "Error manipulando {RequestName} depois de {ElapsedMilliseconds}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);
            return (TResponse)(object)new ErrorResult(["Erro interno do servidor."],
                HttpStatusCode.InternalServerError);
        }
    }
}
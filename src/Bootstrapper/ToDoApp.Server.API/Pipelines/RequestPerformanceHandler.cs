using System.Diagnostics;
using MediatR;

namespace ToDoApp.Server.API.Pipelines;

public class RequestPerformanceHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public RequestPerformanceHandler(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var response = await next();
        stopWatch.Stop();

        _logger.LogInformation("Executed action of type {Type} in {Time}ms", typeof(TRequest).Name, stopWatch.ElapsedMilliseconds);

        return response;
    }
}

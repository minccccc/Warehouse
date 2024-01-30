using Application.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogNewRequestInfoMessage(request.GetType().Name);
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogHandlerPipelineExceptionMessage(ex.Message);
            throw;
        }
    }
}
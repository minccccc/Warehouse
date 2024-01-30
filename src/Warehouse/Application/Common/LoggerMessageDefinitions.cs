using Microsoft.Extensions.Logging;

namespace Application.Common;

public static partial class LoggerMessageDefinitions
{
    [LoggerMessage(EventId = 1000, Level = LogLevel.Error,
        Message = "New Request: {Request}")]
    public static partial void LogNewRequestInfoMessage(this ILogger logger, string request);

    [LoggerMessage(EventId = 9998, Level = LogLevel.Error,
        Message = "Failed to retrieve products: {errorMessage}.")]
    public static partial void LogrRetrieveProductsFailure(this ILogger logger, string errorMessage);

    [LoggerMessage(EventId = 9999, Level = LogLevel.Error,
        Message = "Handler pipeline failure: {errorMessage}.")]
    public static partial void LogHandlerPipelineExceptionMessage(this ILogger logger, string errorMessage);
}
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger
{
    public static partial class LoggerMessageDefinitions
    {
        [LoggerMessage(EventId = 1000, Level = LogLevel.Error,
            Message = "New Request: {Request}")]
        public static partial void LogNewRequestInfoMessage(this ILogger logger, string request);

        [LoggerMessage(EventId = 9999, Level = LogLevel.Error,
            Message = "Handler pipeline failure: {errorMessage}.")]
        public static partial void LogHandlerPipelineExceptionMessage(this ILogger logger, string errorMessage);
    }
}

using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger
{
    public static partial class LoggerMessageDefinitions
    {
        [LoggerMessage(EventId = 1001, Level = LogLevel.Information,
            Message = "Retrieving information from the source...")]
        public static partial void LogRetievingProductstInfoMessage(this ILogger logger);

        [LoggerMessage(EventId = 1002, Level = LogLevel.Information,
            Message = "Query request with MaxPrice: {maxPrice}, MinPrice: {minPrice}, and Size: {size}.")]
        public static partial void LogRequestInfoMessage(this ILogger logger, double? minPrice, double? maxPrice, string size);

        [LoggerMessage(EventId = 2001, Level = LogLevel.Error,
            Message = "Failed to validate query request parameters: {validationErrors}.")]
        public static partial void LogRequestValidationExceptionMessage(this ILogger logger, string validationErrors);

        [LoggerMessage(EventId = 2002, Level = LogLevel.Error,
            Message = "Failed to retrieve products information from the source: {ErrorMessage}.")]
        public static partial void LogFailedToRetrieveProductsExceptionMessage(this ILogger logger, string errorMessage);
    }
}

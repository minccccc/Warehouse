using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.Logger.Configurtion
{
    public static class LoggerExtensions
    {
        public static void AddLogger(this ILoggingBuilder loggingBuilder, ConfigurationManager config)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);
        }
    }
}

using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.ErrorHandling
{
    public static class ExceptionHandlerExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder application)
        {
            application.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is ModelValidationException error)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(error.Errors
                        .Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        }));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    context.Response.ContentType = Text.Plain;
                    await context.Response.WriteAsync("General exception was thrown.");
                }
            });
        }
    }
}

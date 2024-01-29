using Application.Common.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is ModelValidationException error)
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
        }
    }
}

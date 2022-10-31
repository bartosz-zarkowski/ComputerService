using ComputerService.Exceptions;
using ComputerService.Models;
using System.Net;

namespace ComputerService.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong: ");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorModelMessage = new ErrorDetailsModel();
        switch (exception)
        {
            case BadRequestException badRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorModelMessage.StatusCode = (int)HttpStatusCode.BadRequest;
                errorModelMessage.Message = "Bad request.";
                errorModelMessage.Errors = badRequestException.Errors;
                errorModelMessage.Source = badRequestException.Source;
                errorModelMessage.StackTrace = badRequestException.StackTrace;
                break;

            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorModelMessage.StatusCode = (int)HttpStatusCode.NotFound;
                errorModelMessage.Message = exception.Message;
                errorModelMessage.Source = exception.Source;
                errorModelMessage.StackTrace = exception.StackTrace;
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (!_hostEnvironment.IsDevelopment())
                {
                    errorModelMessage.StatusCode = context.Response.StatusCode;
                    errorModelMessage.Message = "Something went wrong.";
                }
                else
                {
                    errorModelMessage.StatusCode = context.Response.StatusCode;
                    errorModelMessage.Message = exception.Message;
                    errorModelMessage.Source = exception.Source;
                    errorModelMessage.StackTrace = exception.StackTrace;
                }
                break;
        }
        await context.Response.WriteAsync(errorModelMessage.ToString());
    }
}

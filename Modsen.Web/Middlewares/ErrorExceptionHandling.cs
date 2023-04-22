using System.Net;
using Modsen.Database.Exceptions;
using Modsen.Domain.Dto;

namespace Modsen.Web.Middlewares;

public class ErrorExceptionHandling
{
    private readonly ILogger<ErrorExceptionHandling> _logger;

    private readonly RequestDelegate _requestDelegate;

    public ErrorExceptionHandling(RequestDelegate requestDelegate, ILogger<ErrorExceptionHandling> logger)
    {
        _requestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (BookExistsException e)
        {
            await HandleExceptionAsync(context,
                e.Message,
                HttpStatusCode.UnprocessableEntity,
                e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            await HandleExceptionAsync(context,
                e.Message,
                HttpStatusCode.InternalServerError,
                e.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode httpStatusCode,
                                            string message)
    {
        _logger.LogError(exMsg);

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int) httpStatusCode;

        var errorDto = new ErrorDto
        {
            StatusCode = (int) httpStatusCode,
            Message = message
        };

        await response.WriteAsJsonAsync(errorDto.ToString());
    }
}
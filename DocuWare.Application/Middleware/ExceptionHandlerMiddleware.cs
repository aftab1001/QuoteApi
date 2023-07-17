using System.Net;
using DocuWare.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DocuWare.Application.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        int httpStatusCode;
        var result = exception.Message;

        switch (exception)
        {
            case NotFoundException:
                httpStatusCode = (int) HttpStatusCode.NotFound;
                break;
            case UserFriendlyException userFriendlyException:
                httpStatusCode = (int) HttpStatusCode.InternalServerError;
                result = userFriendlyException.Message;
                break;
            default:
                httpStatusCode = (int) HttpStatusCode.InternalServerError;
                break;
        }


        _logger.LogError(result);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = httpStatusCode;

        if (result == string.Empty)
            result = JsonConvert.SerializeObject(new {StatusCode = httpStatusCode, error = exception.Message});

        return context.Response.WriteAsync(result);
    }
}
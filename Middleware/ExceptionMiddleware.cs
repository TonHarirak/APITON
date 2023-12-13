using System;
using System.Net;
using System.Text.Json;
using APITON.Errors;

namespace APITON.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment, RequestDelegate requestDelegate)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _requestDelegate = requestDelegate;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (System.Exception e)
        {
            _logger.LogError(e, e.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _hostEnvironment.IsDevelopment()
                ? new ApiException(httpContext.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new ApiException(httpContext.Response.StatusCode, e.Message, "InternalServerErrors");

            var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, opt);

            await httpContext.Response.WriteAsync(json);
        }
    }
}

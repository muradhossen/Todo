using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Application.Common;

namespace Application.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _evn;

    public ExceptionMiddleware(RequestDelegate next
        , ILogger<ExceptionMiddleware> logger
        , IHostEnvironment evn)
    {
        _next = next;
        _logger = logger;
        _evn = evn;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _evn.IsDevelopment() ? Result.Failure($"{ex.Message} \n {ex.StackTrace?.ToString()}") : Result.Failure("Internal server Error");

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, option);

            await context.Response.WriteAsync(json);
        }
    }
}

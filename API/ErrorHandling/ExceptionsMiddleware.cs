using System.Net;
using System.Text.Json;

namespace API.ErrorHandling;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            
            if (e is NotFoundException)
            {
                await NotFoundException(e, context);
            }
           
        }
    }

    private async Task NotFoundException(Exception e, HttpContext context)
    {
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.NotFound;

        var response = _env.IsDevelopment()
            ? new ApiException(context.Response.StatusCode, e.Message, e.StackTrace?.ToString())
            : new ApiException(context.Response.StatusCode, e.Message, "Entity not found.");

        var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
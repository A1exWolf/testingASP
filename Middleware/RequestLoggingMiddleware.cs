namespace TestingBackend.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"HTTP request received: {context.Request.Method} {context.Request.Path}");
        await _next(context);
        _logger.LogInformation($"HTTP response sent: {context.Response.StatusCode}");
    }
}
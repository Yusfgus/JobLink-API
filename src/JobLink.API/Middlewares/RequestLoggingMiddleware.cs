namespace JobLink.API.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        // This runs AFTER the endpoint finishes
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(
            $"\n[{DateTime.Now}] {context.Request.Method} {context.Request.Path} -> {context.Response.StatusCode}\n");
        Console.ResetColor();
    }
}
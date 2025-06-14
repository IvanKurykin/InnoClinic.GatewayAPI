namespace GatewayAPI;

public class JwtCookieToHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieToHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies["jwt"];

        if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Request.Headers.Append("Authorization", $"Bearer {token}");
        }

        await _next(context);
    }
}
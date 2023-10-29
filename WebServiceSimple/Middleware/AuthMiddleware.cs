using DataLayer;

namespace WebServiceSimple.Middleware;

public static class AuthMiddlewareExt
{
    public static void UseAuth(this WebApplication app)
    {
        app.UseMiddleware<AuthMiddleware>();
    }
}

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDataService _dataService;

    public AuthMiddleware(RequestDelegate next, IDataService dataService)
    {
        _next = next;
        _dataService = dataService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userName = context.Request.Headers.Authorization.FirstOrDefault();

        if(userName != null)
        {
            var user = _dataService.GetUser(userName);
            if(user != null) {
                context.Items["User"] = user;
            }
        }
        await _next(context);
    }
}

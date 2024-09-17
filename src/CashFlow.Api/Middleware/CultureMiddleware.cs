namespace CashFlow.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
        
    }
    
    public async Task Invoke(HttpContext context)
    {
        
    }
}
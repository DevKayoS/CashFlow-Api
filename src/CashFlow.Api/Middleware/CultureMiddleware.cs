using System.Globalization;

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
        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        // colocando o ingles como linguagem default
        var cultureInfo = new CultureInfo("en");
        
        if (string.IsNullOrWhiteSpace(culture) == false)
        {
            cultureInfo = new CultureInfo(culture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        // permitindo o fluxo continuar
        await _next(context);
    }
}
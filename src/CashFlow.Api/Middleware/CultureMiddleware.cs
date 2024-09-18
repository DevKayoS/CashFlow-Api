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
        //pegando todas as linguas que o .NET da suporte e salvando em uma lista
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        // colocando o ingles como linguagem default
        var cultureInfo = new CultureInfo("en");
        
        // verificando se veio alguma lingua no header e se ela e suportada no .NET verificando se esta dentro da lista de linguas
        if (string.IsNullOrWhiteSpace(requestedCulture) == false 
            && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        // permitindo o fluxo continuar
        await _next(context);
    }
}
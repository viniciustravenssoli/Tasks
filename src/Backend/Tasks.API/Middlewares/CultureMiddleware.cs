using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.API.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IList<string> _supportedLanguages = new List<string>
    {
        "pt",
        "en"
    };
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var culture = new CultureInfo("pt");

        if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var language = context.Request.Headers["Accept-Language"].ToString();
            if (_supportedLanguages.Any(c => c.Equals(language)))
            {
                culture = new CultureInfo(language);
            }
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}

using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Security.Claims;

namespace HelpDeskPro.Middleware
{
    public class UserLanguageCultureMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var lang = context.User?.FindFirst("lang")?.Value
                       ?? context.User?.FindFirst("locale")?.Value
                       ?? context.User?.FindFirst(ClaimTypes.Locality)?.Value
                       ?? context.Request.Headers.AcceptLanguage.ToString()?.Split(',').FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(lang))
            {
                try
                {
                    var culture = new CultureInfo(lang);
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                    context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(new RequestCulture(culture), null));
                }
                catch (CultureNotFoundException)
                {
                    // ignore invalid cultures
                }
            }

            await next(context);
        }
    }

    public static class UserLanguageCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLanguageCulture(this IApplicationBuilder app)
            => app.UseMiddleware<UserLanguageCultureMiddleware>();
    }
}

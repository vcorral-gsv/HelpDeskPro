using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Security.Claims;

namespace HelpDeskPro.Middleware
{
    /// <summary>
    /// Middleware que ajusta la cultura actual según claims del usuario o cabecera Accept-Language.
    /// </summary>
    public class UserLanguageCultureMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Invoca el siguiente middleware tras establecer la cultura si es válida.
        /// </summary>
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

    /// <summary>
    /// Extensiones para registrar el middleware de cultura de usuario.
    /// </summary>
    public static class UserLanguageCultureMiddlewareExtensions
    {
        /// <summary>
        /// Registra el middleware que establece cultura de usuario.
        /// </summary>
        public static IApplicationBuilder UseUserLanguageCulture(this IApplicationBuilder app)
            => app.UseMiddleware<UserLanguageCultureMiddleware>();
    }
}

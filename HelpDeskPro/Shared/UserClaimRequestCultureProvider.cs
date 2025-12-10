using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace HelpDeskPro.Shared
{
    public class UserClaimRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(
            HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated != true)
                return Task.FromResult<ProviderCultureResult?>(null);

            // Prioridad de claims: lang -> locale -> custom claim type
            var lang = user.FindFirst("lang")?.Value
                      ?? user.FindFirst("locale")?.Value
                      ?? user.FindFirst("language")?.Value;

            if (string.IsNullOrWhiteSpace(lang))
                return Task.FromResult<ProviderCultureResult?>(null);

            // Puedes normalizar aquí (ej: "es" -> "es-ES") si quieres
            var culture = new CultureInfo(lang);

            var result = new ProviderCultureResult(culture.Name, culture.Name);
            return Task.FromResult<ProviderCultureResult?>(result);
        }
    }
}

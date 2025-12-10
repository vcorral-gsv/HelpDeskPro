using HelpDeskPro.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace HelpDeskPro.Extensions
{
    /// <summary>
    /// Extensiones para registrar autenticación y autorización JWT.
    /// </summary>
    public static class AuthServiceCollectionExtensions
    {
        /// <summary>
        /// Configura JWT Bearer y política por defecto autenticada.
        /// </summary>
        public static IServiceCollection AddHelpDeskProAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Evita la respuesta por defecto de ASP.NET Core
                            context.HandleResponse();

                            var response = context.Response;
                            response.StatusCode = StatusCodes.Status401Unauthorized;
                            response.ContentType = "application/json";

                            var payload = new ApiErrorResponse
                            {
                                Descripcion = "Unauthorized",
                                Path = context.Request.Path,
                                Method = context.Request.Method,
                                StatusCode = StatusCodes.Status401Unauthorized,
                                TraceId = context.HttpContext.TraceIdentifier
                            };

                            await response.WriteAsJsonAsync(payload);
                        },

                        OnForbidden = async context =>
                        {
                            var response = context.Response;
                            response.StatusCode = StatusCodes.Status403Forbidden;
                            response.ContentType = "application/json";

                            var payload = new ApiErrorResponse
                            {
                                Descripcion = "Forbidden",
                                Path = context.Request.Path,
                                Method = context.Request.Method,
                                StatusCode = StatusCodes.Status403Forbidden,
                                TraceId = context.HttpContext.TraceIdentifier
                            };

                            await response.WriteAsJsonAsync(payload);
                        }
                    };

                    var secret = configuration["Authentication:SecretForKey"]
                                 ?? throw new InvalidOperationException("Authentication:SecretForKey not configured.");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Convert.FromBase64String(secret))
                    };
                });

            services
                .AddAuthorizationBuilder()
                .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build());

            return services;
        }
    }
}

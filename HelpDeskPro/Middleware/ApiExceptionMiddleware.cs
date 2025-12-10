using HelpDeskPro.Dtos;
using HelpDeskPro.Exceptions;
using HelpDeskPro.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core.Exceptions;
using System.Net;

namespace HelpDeskPro.Middleware
{
    /// <summary>
    /// Middleware para manejo centralizado de excepciones y respuestas de error JSON.
    /// </summary>
    public class ApiExceptionMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Invoca el siguiente middleware y captura excepciones para responder con estructura uniforme.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            // Excepciones de dominio primero (más específicas)

            catch (ForbiddenException ex)
            {
                await WriteErrorAsync(
                    context,
                    GetTranslation("Forbidden", ex.Message),
                    ex,
                    HttpStatusCode.Forbidden);
            }

            catch (ValidationDomainException ex)
            {
                // Aquí sí tiene sentido mostrar el mensaje de la excepción
                await WriteErrorAsync(
                    context,
                    ex.Message,
                    ex,
                    HttpStatusCode.BadRequest);
            }

            catch (ConflictException ex)
            {
                await WriteErrorAsync(
                    context,
                    GetTranslation("Conflict", ex.Message),
                    ex,
                    HttpStatusCode.Conflict);
            }

            catch (ParseException ex)
            {
                await WriteErrorAsync(context, GetTranslation("Blank_Order_Field"), ex, HttpStatusCode.BadRequest);
            }
            catch (BadHttpRequestException ex)
            {
                await WriteErrorAsync(context, GetTranslation("Bad_Request"), ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentNullException ex)
            {
                await WriteErrorAsync(context, GetTranslation("Argument_Null"), ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentException ex)
            {
                await WriteErrorAsync(context, GetTranslation("Argument_Invalid"), ex, HttpStatusCode.BadRequest);
            }
            catch (KeyNotFoundException ex)
            {
                // Incluye también NotFoundException
                await WriteErrorAsync(context, GetTranslation("Not_Found"), ex, HttpStatusCode.NotFound);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Incluye UnauthorizedDomainException
                await WriteErrorAsync(context, GetTranslation("Unauthorized"), ex, HttpStatusCode.Unauthorized);
            }
            catch (InvalidOperationException ex)
            {
                string description = NormalizeInvalidOperationMessage(ex);
                await WriteErrorAsync(context, description, ex, HttpStatusCode.BadRequest);
            }
            catch (DbUpdateException ex)
            {
                await WriteErrorAsync(context, GetTranslation("Unexpected_Error"), ex, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await WriteErrorAsync(context, GetTranslation("Unexpected_Error"), ex, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Obtiene una traducción por clave desde recursos, con fallback.
        /// </summary>
        public static string GetTranslation(string key, string? fallback = null, params string[] translationParams)
        {
            var translation = MsgTranslationsResource.ResourceManager.GetString(key);

            return translation is null
                ? fallback ?? key
                : translationParams is { Length: > 0 }
                ? string.Format(translation, translationParams)
                : translation;
        }

        /// <summary>
        /// Normaliza el mensaje de InvalidOperationException a una forma traducible.
        /// </summary>
        private static string NormalizeInvalidOperationMessage(InvalidOperationException ex)
        {
            return ex.Message.Contains("No route matches the supplied values.")
                ? GetTranslation("NoRouteMatches", "No route matches the supplied values.")
                : GetTranslation("Unexpected_Error", "An unexpected error occurred.");
        }

        /// <summary>
        /// Escribe la respuesta JSON de error con metadatos del request y trazas.
        /// </summary>
        private static async Task WriteErrorAsync(HttpContext context, string description, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var response = new ApiErrorResponse
            {
                Descripcion = description,
                Path = context.Request.Path,
                Method = context.Request.Method,
                ExceptionSerialized = $"ExceptionInfo{{Type: {ex.GetType().FullName}; Message: {ex.Message}; Source: {ex.Source}}}",
                StatusCode = (int)statusCode,
                TraceId = context.TraceIdentifier,
                ErrorCodes = ex is IWithErrorCodes codes ? codes.ErrorCodes : null
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }

    /// <summary>
    /// Extensiones para registrar el middleware de excepciones.
    /// </summary>
    public static class ApiExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Registra el middleware de excepciones en la tubería de la aplicación.
        /// </summary>
        public static IApplicationBuilder UseApiExceptionMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ApiExceptionMiddleware>();
    }
}

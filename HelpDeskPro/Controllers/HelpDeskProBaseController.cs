using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelpDeskPro.Controllers
{
    /// <summary>
    /// Controlador base para la API HelpDeskPro. Proporciona utilidades comunes y configuración base.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class HelpDeskProBaseController<T>(ILogger<T> logger, IMapper mapper) : ControllerBase
    {
        protected readonly ILogger<T> _logger = logger;
        protected readonly IMapper _mapper = mapper;

        /// <summary>
        /// Obtiene el email del usuario autenticado desde los claims.
        /// </summary>
        /// <returns>Email del usuario o cadena vacía</returns>
        protected string GetClientEmail()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Obtiene el identificador del usuario autenticado desde los claims.
        /// </summary>
        /// <returns>Identificador del usuario o cadena vacía</returns>
        protected string GetClientId()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        /// <summary>
        /// Obtiene el rol del usuario autenticado desde los claims.
        /// </summary>
        /// <returns>Rol del usuario o cadena vacía</returns>
        protected string GetClientRole()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        }
        /// <summary>
        /// Obtiene el código de idioma del usuario autenticado desde los claims.
        /// </summary>
        /// <returns>Código de idioma (locale) o cadena vacía</returns>
        protected string GetClientLocale()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == "locale")?.Value ?? string.Empty;
        }
    }
}

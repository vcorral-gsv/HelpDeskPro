using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelpDeskPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class HelpDeskProBaseController<T>(ILogger<T> logger, IMapper mapper) : ControllerBase
    {
        protected readonly ILogger<T> _logger = logger;
        protected readonly IMapper _mapper = mapper;

        protected string GetClientEmail()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }

        protected string GetClientId()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
        protected string GetClientRole()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        }
        protected string GetClientLocale()
        {
            return User.Claims.FirstOrDefault( c=> c.Type == "locale")?.Value ?? string.Empty;
        }
    }
}

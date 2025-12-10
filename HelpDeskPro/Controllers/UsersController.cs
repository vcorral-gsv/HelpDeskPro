using AutoMapper;
using HelpDeskPro.Criterias.Users;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.User;
using HelpDeskPro.Services.UserService;
using HelpDeskPro.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UsersController(IMapper _mapper, ILogger<UsersController> _logger, IUserService _userService) : HelpDeskProBaseController<UsersController>(_logger, _mapper)
    {
        /// <summary>
        /// Obtiene usuarios paginados y filtrados.
        /// </summary>
        /// <param name="request">Filtros y paginación</param>
        /// <returns>Resultado paginado con usuarios</returns>
        /// <response code="200">Listado paginado de usuarios</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GenericPaginationOutputDto<ListUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GenericPaginationOutputDto<ListUserDto>>> GetUsers([FromBody] GetUsersFiltersInputDto request)
        {
            var result = await _userService.GetUsersAsync(request);

            ResponseHeadersUtils.SetHeader_XPagination(Response, result.PaginationMetadata);

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Usuario detallado si existe</returns>
        /// <response code="200">Usuario encontrado</response>
        /// <response code="404">Usuario no encontrado</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DetailUserDto>> GetUserById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        /// <summary>
        /// Obtiene un usuario por su email.
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <returns>Usuario detallado si existe</returns>
        /// <response code="200">Usuario encontrado</response>
        /// <response code="404">Usuario no encontrado</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpGet("by-email/{email}")]
        [ProducesResponseType(typeof(DetailUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DetailUserDto>> GetUserByEmail(string email)
        {
            var userDto = await _userService.GetUserByEmailAsync(email);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        /// <summary>
        /// Crea un usuario.
        /// </summary>
        /// <param name="request">Datos del nuevo usuario</param>
        /// <returns>Usuario creado</returns>
        /// <response code="201">Usuario creado</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(DetailUserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> CreateUser([FromBody] AddUserDto request)
        {
            var userDto = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Obtiene usuarios agrupados por rol.
        /// </summary>
        /// <param name="request">Paginación</param>
        /// <returns>Grupos de usuarios por rol</returns>
        /// <response code="200">Listado de roles con usuarios</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpPost("get-grouped-by-role")]
        [ProducesResponseType(typeof(GenericPaginationOutputDto<ListRolesWithUsersDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GenericPaginationOutputDto<ListRolesWithUsersDto>>> GetUsersGroupedByRole([FromBody] PaginationInputDto request)
        {
            var result = await _userService.GetUsersGroupedByRoleAsync(request);
            ResponseHeadersUtils.SetHeader_XPagination(Response, result.PaginationMetadata);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene usuarios agrupados por equipo.
        /// </summary>
        /// <param name="request">Paginación</param>
        /// <returns>Grupos de usuarios por equipo</returns>
        /// <response code="200">Listado de equipos con usuarios</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpPost("get-grouped-by-team")]
        [ProducesResponseType(typeof(GenericPaginationOutputDto<ListUsersGroupedByTeamDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GenericPaginationOutputDto<ListUsersGroupedByTeamDto>>> GetUsersGroupedByTeam([FromBody] PaginationInputDto request)
        {
            var result = await _userService.GetUsersGroupedByTeamAsync(request);
            ResponseHeadersUtils.SetHeader_XPagination(Response, result.PaginationMetadata);
            return Ok(result);
        }
    }
}

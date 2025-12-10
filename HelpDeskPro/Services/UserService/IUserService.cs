using HelpDeskPro.Criterias.Users;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.User;

namespace HelpDeskPro.Services.UserService
{
    /// <summary>
    /// Servicio de usuarios para consulta y creación.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Obtiene usuarios paginados aplicando filtros.
        /// </summary>
        /// <param name="request">Filtros y paginación</param>
        /// <returns>Resultado paginado de usuarios</returns>
        Task<GenericPaginationOutputDto<ListUserDto>> GetUsersAsync(
            GetUsersFiltersInputDto request
            );
        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Usuario detallado o null</returns>
        Task<DetailUserDto?> GetUserByIdAsync(int id);
        /// <summary>
        /// Obtiene un usuario por su email.
        /// </summary>
        /// <param name="email">Correo electrónico</param>
        /// <returns>Usuario detallado o null</returns>
        Task<DetailUserDto?> GetUserByEmailAsync(string email);
        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="request">Datos del usuario</param>
        /// <returns>Usuario creado</returns>
        Task<DetailUserDto> CreateUserAsync(AddUserDto request);
        /// <summary>
        /// Obtiene usuarios agrupados por rol con paginación.
        /// </summary>
        /// <param name="request">Paginación</param>
        /// <returns>Grupos de usuarios por rol</returns>
        Task<GenericPaginationOutputDto<ListRolesWithUsersDto>> GetUsersGroupedByRoleAsync(PaginationInputDto request);
        /// <summary>
        /// Obtiene usuarios agrupados por equipo con paginación.
        /// </summary>
        /// <param name="request">Paginación</param>
        /// <returns>Grupos de usuarios por equipo</returns>
        Task<GenericPaginationOutputDto<ListUsersGroupedByTeamDto>> GetUsersGroupedByTeamAsync(PaginationInputDto request);


    }
}

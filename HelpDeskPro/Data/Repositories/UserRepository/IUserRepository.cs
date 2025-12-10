using HelpDeskPro.Criterias;
using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.UserRepository
{
    /// <summary>
    /// Contrato del repositorio de usuarios.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>Obtiene un usuario por su Id.</summary>
        /// <param name="id">El Id del usuario.</param>
        /// <returns>Un usuario.</returns>
        Task<User?> GetByIdAsync(int id);

        /// <summary>Obtiene un usuario por su email.</summary>
        /// <param name="email">El email del usuario.</param>
        /// <returns>Un usuario.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>Agrega un usuario a la persistencia.</summary>
        /// <param name="user">El usuario a agregar.</param>
        Task AddAsync(User user);

        /// <summary>Indica si existe un usuario con el email dado.</summary>
        /// <param name="email">El email a verificar.</param>
        /// <returns>true si el email existe, false en caso contrario.</returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>Guarda los cambios en la persistencia.</summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Obtiene usuarios con paginación y criterios de filtrado.
        /// </summary>
        /// <param name="pageNumber">El número de página.</param>
        /// <param name="pageSize">El tamaño de página.</param>
        /// <param name="criteria">Los criterios de filtrado.</param>
        /// <returns>Usuarios paginados.</returns>
        Task<PagedResult<User>> GetUsersAsync(
            int pageNumber,
            int pageSize,
            IReadOnlyList<CriteriaBase<User>> criteria
        );

        /// <summary>
        /// Obtiene usuarios agrupados por rol con paginación.
        /// </summary>
        /// <param name="pageNumber">El número de página.</param>
        /// <param name="pageSize">El tamaño de página.</param>
        /// <returns>Usuarios agrupados por rol.</returns>
        Task<PagedResult<UsersByRoleGroup>> GetUsersGroupedByRoleAsync(
              int pageNumber,
              int pageSize
        );

        /// <summary>
        /// Obtiene usuarios agrupados por equipo con paginación.
        /// </summary>
        /// <param name="pageNumber">El número de página.</param>
        /// <param name="pageSize">El tamaño de página.</param>
        /// <returns>Usuarios agrupados por equipo.</returns>
        Task<PagedResult<UsersByTeamGroup>> GetUsersGroupedByTeamAsync(
              int pageNumber,
              int pageSize
        );
    }
}

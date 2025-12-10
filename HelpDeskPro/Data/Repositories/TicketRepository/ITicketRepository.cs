using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.TicketRepository
{
    /// <summary>
    /// Contrato del repositorio de tickets.
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Obtiene todos los tickets con paginación.
        /// </summary>
        Task<PagedResult<Ticket>> GetAllTicketsAsync(int page, int pageSize);
        /// <summary>
        /// Obtiene tickets agrupados por su estado.
        /// </summary>
        Task<IReadOnlyList<TicketsByStatus>> GetTicketsGroupedByStatusAsync();
    }
}

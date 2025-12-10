using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Services.TicketService
{
    /// <summary>
    /// Servicio de tickets para listados y agrupaciones.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Obtiene todos los tickets paginados.
        /// </summary>
        /// <param name="req">Parámetros de paginación</param>
        /// <returns>Resultado paginado con tickets</returns>
        Task<GenericPaginationOutputDto<ListTicketDto>> GetAllTicketsAsync(PaginationInputDto req);
        /// <summary>
        /// Obtiene los tickets agrupados por estado.
        /// </summary>
        /// <returns>Listado de grupos por estado</returns>
        Task<List<ListTicketsGroupedByStatus>> GetTicketsGroupedByStatusAsync();
    }
}

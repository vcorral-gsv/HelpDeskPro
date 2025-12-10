using AutoMapper;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.Ticket;
using HelpDeskPro.Services.TicketService;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro.Controllers
{
    /// <summary>
    /// Controller para la gestión de tickets.
    /// </summary>
    public class TicketsController(IMapper _mapper, ILogger<TicketsController> _logger, ITicketService _ticketService) : HelpDeskProBaseController<TicketsController>(_logger, _mapper)
    {
        /// <summary>
        /// Obtiene todos los tickets paginados.
        /// </summary>
        /// <param name="req">Parámetros de paginación</param>
        /// <returns>Listado paginado de tickets</returns>
        /// <response code="200">Listado de tickets</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericPaginationOutputDto<ListTicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GenericPaginationOutputDto<ListTicketDto>>> GetAllTickets([FromQuery] PaginationInputDto req)
        {
            var tickets = await _ticketService.GetAllTicketsAsync(req);
            return Ok(tickets);
        }

        /// <summary>
        /// Obtiene tickets agrupados por estado.
        /// </summary>
        /// <returns>Listado de tickets agrupados por estado</returns>
        /// <response code="200">Listado agrupado por estado</response>
        /// <response code="401">No autenticado</response>
        /// <response code="403">No autorizado</response>
        [HttpGet("grouped-by-status")]
        [ProducesResponseType(typeof(List<ListTicketsGroupedByStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ListTicketsGroupedByStatus>>> GetTicketsGroupedByStatus()
        {
            var groupedTickets = await _ticketService.GetTicketsGroupedByStatusAsync();
            return Ok(groupedTickets);
        }
    }
}

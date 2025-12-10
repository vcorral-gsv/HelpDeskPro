using AutoMapper;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.Ticket;
using HelpDeskPro.Services.TicketService;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro.Controllers
{
    public class TicketsController(IMapper _mapper, ILogger<TicketsController> _logger, ITicketService _ticketService) : HelpDeskProBaseController<TicketsController>(_logger, _mapper)
    {
        [HttpGet]
        public async Task<ActionResult<GenericPaginationOutputDto<ListTicketDto>>> GetAllTickets([FromQuery] PaginationInputDto req)
        {
            var tickets = await _ticketService.GetAllTicketsAsync(req);
            return Ok(tickets);
        }
        [HttpGet("grouped-by-status")]
        public async Task<ActionResult<List<ListTicketsGroupedByStatus>>> GetTicketsGroupedByStatus()
        {
            var groupedTickets = await _ticketService.GetTicketsGroupedByStatusAsync();
            return Ok(groupedTickets);
        }
    }
}

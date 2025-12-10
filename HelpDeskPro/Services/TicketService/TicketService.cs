using AutoMapper;
using HelpDeskPro.Data.Repositories.TicketRepository;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Services.TicketService
{
    public class TicketService(
        ITicketRepository _ticketRepository,
        ILogger<TicketService> _logger,
        IMapper _mapper
    ) : ITicketService
    {
        public async Task<GenericPaginationOutputDto<ListTicketDto>> GetAllTicketsAsync(PaginationInputDto req)
        {
            var page = await _ticketRepository.GetAllTicketsAsync(req.Page, req.PageSize);

            var ticketDtos = _mapper.Map<List<ListTicketDto>>(page.Items);

            var paginationMetadata = new PaginationOutputDto
            {
                CurrentPage = req.Page,
                PageSize = req.PageSize,
                PageItems = page.Items.Count,
                TotalItems = page.TotalCount,
                TotalPages = (int)Math.Ceiling(page.TotalCount / (double)req.PageSize)
            };

            return new GenericPaginationOutputDto<ListTicketDto>(
                ticketDtos,
                paginationMetadata
            );
        }
        public async Task<List<ListTicketsGroupedByStatus>> GetTicketsGroupedByStatusAsync()
        {
            var res = await _ticketRepository.GetTicketsGroupedByStatusAsync();
               
            var dtos = res.Select(group => new ListTicketsGroupedByStatus
            {
                Status = group.StatusName,
                Tickets = _mapper.Map<List<ListTicketDto>>(group.Tickets)
            }).ToList();

            return dtos;
        }
    }
}

using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Services.TicketService
{
    public interface ITicketService
    {
        Task<GenericPaginationOutputDto<ListTicketDto>> GetAllTicketsAsync(PaginationInputDto req);
        Task<List<ListTicketsGroupedByStatus>> GetTicketsGroupedByStatusAsync();
    }
}

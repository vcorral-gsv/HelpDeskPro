using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.TicketRepository
{
    public interface ITicketRepository
    {
        Task<PagedResult<Ticket>> GetAllTicketsAsync(int page, int pageSize);
        Task<IReadOnlyList<TicketsByStatus>> GetTicketsGroupedByStatusAsync();
    }
}

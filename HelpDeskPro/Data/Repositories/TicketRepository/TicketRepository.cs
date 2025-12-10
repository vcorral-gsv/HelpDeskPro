using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro.Data.Repositories.TicketRepository
{
    public class TicketRepository(HelpDeskProContext _context) : RepositoryBase<Ticket>(_context), ITicketRepository
    {
        public async Task<PagedResult<Ticket>> GetAllTicketsAsync(int page, int pageSize)
        {
            IQueryable<Ticket> query = _set.AsNoTracking();
            return await ToPagedResultAsync(query, page, pageSize);
        }

        public async Task<IReadOnlyList<TicketsByStatus>> GetTicketsGroupedByStatusAsync()
        {
            var query = _set.AsNoTracking().GroupBy(t => t.Status);
            var result = query.Select(g => new TicketsByStatus
            {
                StatusName = g.Key.ToString(),
                Tickets = g.ToList()
            });
            return await result.ToListAsync();
        }
    }
}

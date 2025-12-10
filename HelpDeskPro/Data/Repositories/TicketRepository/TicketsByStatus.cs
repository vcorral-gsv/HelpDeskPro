using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.TicketRepository
{
    public sealed class TicketsByStatus
    {
        public required string StatusName { get; init; }
        public required IReadOnlyList<Ticket> Tickets { get; init; }
    }

}

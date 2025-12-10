namespace HelpDeskPro.Dtos.Ticket
{
    public class ListTicketsGroupedByStatus
    {
        public required string Status { get; set; }
        public required List<ListTicketDto> Tickets { get; set; }
    }
}

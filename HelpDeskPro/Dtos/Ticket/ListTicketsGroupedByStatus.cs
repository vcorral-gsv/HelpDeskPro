namespace HelpDeskPro.Dtos.Ticket
{
    /// <summary>
    /// Grupo de tickets por estado.
    /// </summary>
    public class ListTicketsGroupedByStatus
    {
        /// <summary>Estado agrupador.</summary>
        public required string Status { get; set; }
        /// <summary>Listado de tickets en el estado.</summary>
        public required List<ListTicketDto> Tickets { get; set; }
    }
}

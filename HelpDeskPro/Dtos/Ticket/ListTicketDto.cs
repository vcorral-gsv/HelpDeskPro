namespace HelpDeskPro.Dtos.Ticket
{
    /// <summary>
    /// Ticket para listados.
    /// </summary>
    public class ListTicketDto
    {
        /// <summary>Identificador del ticket.</summary>
        public int Id { get; set; }
        /// <summary>Título del ticket.</summary>
        public required string Title { get; set; }
        /// <summary>Estado actual del ticket.</summary>
        public required string Status { get; set; }
        /// <summary>Fecha de creación.</summary>
        public DateTime CreatedAt { get; set; }
    }
}

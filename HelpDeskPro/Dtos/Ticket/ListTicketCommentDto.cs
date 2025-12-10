namespace HelpDeskPro.Dtos.Ticket
{
    /// <summary>
    /// Comentario asociado a un ticket.
    /// </summary>
    public class ListTicketCommentDto
    {
        /// <summary>Identificador del comentario.</summary>
        public int Id { get; set; }
        /// <summary>Contenido del comentario.</summary>
        public required string Body { get; set; }
        /// <summary>Fecha de creación del comentario.</summary>
        public DateTime CreatedAt { get; set; }
    }
}

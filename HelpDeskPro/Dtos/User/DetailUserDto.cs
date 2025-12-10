using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Dtos.User
{
    /// <summary>
    /// Usuario con información detallada y relaciones.
    /// </summary>
    public class DetailUserDto : ListUserDto
    {
        /// <summary>Nombre.</summary>
        public required string FirstName { get; set; }
        /// <summary>Apellidos.</summary>
        public required string LastName { get; set; }
        /// <summary>Rol del usuario.</summary>
        public required string Role { get; set; }
        /// <summary>Indica si el usuario está activo.</summary>
        public bool IsActive { get; set; }
        /// <summary>Fecha de creación.</summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>Código de idioma preferido.</summary>
        public required string LanguageCode { get; set; }

        // Relaciones

        /// <summary>Equipos a los que pertenece.</summary>
        public List<string> TeamNames { get; set; } = [];
        /// <summary>Tickets creados por el usuario.</summary>
        public ICollection<ListTicketDto> Tickets { get; set; } = [];
        /// <summary>Tickets asignados al usuario.</summary>
        public ICollection<ListTicketDto> AssignedTickets { get; set; } = [];
        /// <summary>Comentarios del usuario.</summary>
        public ICollection<ListTicketCommentDto> Comments { get; set; } = [];
        /// <summary>Adjuntos subidos por el usuario.</summary>
        public ICollection<ListTicketAttachmentDto> Attachments { get; set; } = [];
    }
}

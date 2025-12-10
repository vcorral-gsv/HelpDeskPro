using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Dtos.User
{
    public class DetailUserDto : ListUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string LanguageCode { get; set; }

        // Relaciones

        public List<string> TeamNames { get; set; } = [];
        public ICollection<ListTicketDto> Tickets { get; set; } = [];
        public ICollection<ListTicketDto> AssignedTickets { get; set; } = [];
        public ICollection<ListTicketCommentDto> Comments { get; set; } = [];
        public ICollection<ListTicketAttachmentDto> Attachments { get; set; } = [];
    }
}

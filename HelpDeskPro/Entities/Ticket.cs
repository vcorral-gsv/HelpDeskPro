using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HelpDeskPro.Consts.Tickets;

namespace HelpDeskPro.Entities
{
    public class Ticket
    {
        public Ticket()
        {
            IsDeleted = false;
            Priority = Priorities.Normal;
        }
        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Code (string, required, unique, max 20)
        [Required, MaxLength(20)]
        public required string Code { get; set; }

        // Title (string, required, max 200)
        [Required, MaxLength(200)]
        public required string Title { get; set; }

        // Description (string, required, puede ser largo)
        [Required, MaxLength(1000)]
        public required string Description { get; set; }

        // Status (enum: Open, InProgress, WaitingCustomer, Resolved, Closed)
        [Required]
        public required Statuses Status { get; set; }

        // Priority (enum: Low, Normal, High, Critical)
        public Priorities Priority { get; set; }


        // CategoryId (int, FK → TicketCategory)
        [Required]
        public int CategoryId { get; set; }
        public required TicketCategory Category { get; set; }

        // ReporterId (int, FK → User que crea el ticket)
        [Required]
        public int ReporterId { get; set; }
        public required User Reporter { get; set; }

        // AssigneeId (int?, FK → User asignado, suele ser agente)
        public int? AssigneeId { get; set; }
        public User? Assignee { get; set; }

        // TeamId (int?, FK → Team asignado)
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        // CreatedAt (DateTime, UTC)
        public DateTime CreatedAt { get; set; }

        // UpdatedAt (DateTime, UTC)
        public DateTime UpdatedAt { get; set; }

        // ResolvedAt (DateTime?, UTC)
        public DateTime? ResolvedAt { get; set; }

        // ClosedAt (DateTime?, UTC)
        public DateTime? ClosedAt { get; set; }

        // DueAt (DateTime?, UTC)
        public DateTime? DueAt { get; set; }

        // IsDeleted (bool, default false)
        public bool IsDeleted { get; set; }


        // Relaciones

        // Un ticket tiene muchos TicketComments
        public ICollection<TicketComment> TicketComments { get; set; } = [];

        // Un ticket puede tener muchos TicketAttachments.
        public ICollection<TicketAttachment> TicketAttachments { get; set; } = [];

        //Un ticket puede tener muchos TicketTags (N:N)
        public ICollection<TicketTag> TicketTags { get; set; } = [];

    }
}

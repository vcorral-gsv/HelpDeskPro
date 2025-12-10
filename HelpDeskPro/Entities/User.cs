using HelpDeskPro.Consts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    /// <summary>
    /// Entidad User que representa usuarios del sistema.
    /// </summary>
    public class User
    {
        public User()
        {
            IsActive = true;
            LanguageCode = "en";
        }

        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Email (string, required, unique, max 200)
        [Required, EmailAddress, MaxLength(200)]
        public required string Email { get; set; }

        // PasswordHash (string, required)
        [Required]
        public required string PasswordHash { get; set; }

        // FirstName (string, required, max 100)
        [Required, MaxLength(100)]
        public required string FirstName { get; set; }

        // LastName (string, required, max 100)
        [Required, MaxLength(100)]
        public required string LastName { get; set; }

        // Role (enum: Admin, Agent, Customer)
        public Roles Role { get; set; } = Roles.Customer;

        // IsActive (bool, default true)
        public bool IsActive { get; set; }

        // CreatedAt (DateTime, UTC)
        public DateTime CreatedAt { get; set; }

        // LastLoginAt(DateTime?, UTC)
        public DateTime? LastLoginAt { get; set; }

        // LanguageCode (string, max 5, default "en")
        [MaxLength(5)]
        public string LanguageCode { get; set; }

        // Relaciones

        // Un user (Agent) puede pertenecer a varios Teams.
        public ICollection<Team> Teams { get; set; } = [];

        // Un user puede ser Reporter
        public ICollection<Ticket> ReportedTickets { get; set; } = [];

        //  o Assignee de muchos Tickets
        public ICollection<Ticket> AssignedTickets { get; set; } = [];

        // Un user crea TicketComments
        public ICollection<TicketComment> TicketComments { get; set; } = [];

        // Un user puede tener varios RefreshTokens
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        // Un user puede subir varios TicketAttachments
        public ICollection<TicketAttachment> UploadedAttachments { get; set; } = [];
    }
}

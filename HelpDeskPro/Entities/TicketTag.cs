using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    public class TicketTag
    {
        public TicketTag()
        {
            IsActive = true;
        }

        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Name (string, required, unique, max 50)
        [Required, MaxLength(50)]
        public required string Name { get; set; }

        // Color (string?, max 7, formato HEX tipo #FF0000)
        [MaxLength(7)]
        public string? Color { get; set; }

        // IsActive (bool, default true)
        [Required]
        public bool IsActive { get; set; }

        // CreatedAt (DateTime, UTC)
        [Required]
        public required DateTime CreatedAt { get; set; }

        // Relaciones

        // Relación N:N con Ticket
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}

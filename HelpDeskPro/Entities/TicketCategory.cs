using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    public class TicketCategory
    {
        public TicketCategory()
        {
            IsActive = true;
        }

        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Name (string, required, unique, max 100)
        [Required, MaxLength(100)]
        public required string Name { get; set; }

        // Description (string?, max 300)
        [MaxLength(300)]
        public string? Description { get; set; }

        // IsActive (bool, default true)
        [Required]
        public bool IsActive { get; set; }

        // CreatedAt (DateTime, UTC)
        [Required]
        public required DateTime CreatedAt { get; set; }

        // Relaciones
        // Un TicketCategory puede tener muchos Tickets.
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}

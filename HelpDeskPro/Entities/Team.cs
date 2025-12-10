using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    /// <summary>
    /// Entidad Team que agrupa usuarios para la gestión de tickets.
    /// </summary>
    public class Team
    {
        public Team()
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

        // Description (string?, max 500)
        [MaxLength(500)]
        public string? Description { get; set; }

        // IsActive (bool, default true)
        public bool IsActive { get; set; }

        // CreatedAt (DateTime, UTC)
        public DateTime CreatedAt { get; set; }

        // Relaciones

        // Muchos a muchos con User (solo agentes).
        public ICollection<User> Members { get; set; } = [];
    }
}

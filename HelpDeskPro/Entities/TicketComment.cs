using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    /// <summary>
    /// Entidad TicketComment que almacena comentarios asociados a tickets.
    /// </summary>
    public class TicketComment
    {
        public TicketComment()
        {
            IsInternal = false;
        }

        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // TicketId (int, FK → Ticket)
        [Required]
        public int TicketId { get; set; }
        public required Ticket Ticket { get; set; }

        // AuthorId (int, FK → User)
        [Required]
        public int AuthorId { get; set; }
        public required User Author { get; set; }

        // Body (string, required)
        [Required, MaxLength(2000)]
        public required string Body { get; set; }

        // IsInternal (bool, default false)
        public bool IsInternal { get; set; }

        // CreatedAt (DateTime, UTC)
        public DateTime CreatedAt { get; set; }
    }
}

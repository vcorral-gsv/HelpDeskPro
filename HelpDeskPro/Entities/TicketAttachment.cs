using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{

    /// <summary>
    /// Entidad TicketAttachment que almacena metadatos de archivos asociados a tickets.
    /// </summary>
    public class TicketAttachment
    {
        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // TicketId (int, FK → Ticket)
        [Required]
        public int TicketId { get; set; }
        public required Ticket Ticket { get; set; }

        // FileName (string, required, max 255)
        [Required, MaxLength(255)]
        public required string FileName { get; set; }

        // ContentType (string, required, max 100)
        [Required, MaxLength(100)]
        public required string ContentType { get; set; }

        // FileSizeBytes (long, required)
        [Required]
        public long FileSizeBytes { get; set; }

        // StorageUrl (string, required, max 500) – URL al storage.
        [Required, MaxLength(500)]
        public required string StorageUrl { get; set; }

        // UploadedById (int, FK → User)
        [Required]
        public int UploadedById { get; set; }
        public required User UploadedBy { get; set; }

        // UploadedAt (DateTime, UTC)
        public DateTime UploadedAt { get; set; }
    }
}

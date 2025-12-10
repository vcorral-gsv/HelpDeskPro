using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro.Entities
{
    public class RefreshToken
    {
        // Id (int, PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // UserId (int, FK → User)
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Token (string, required, unique)
        [Required]
        public required string Token { get; set; }

        // ExpiresAt (DateTime, UTC)
        public DateTime ExpiresAt { get; set; }

        // CreatedAt (DateTime, UTC)
        public DateTime CreatedAt { get; set; }

        // RevokedAt (DateTime?, UTC)
        public DateTime? RevokedAt { get; set; }
    }
}

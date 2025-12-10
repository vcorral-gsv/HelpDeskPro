using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Dtos.User
{
    /// <summary>
    /// Usuario en listados.
    /// </summary>
    public class ListUserDto
    {
        /// <summary>Identificador del usuario.</summary>
        public int Id { get; set; }
        /// <summary>Correo electrónico.</summary>
        public required string Email { get; set; }
        /// <summary>Fecha/hora del último inicio de sesión.</summary>
        public DateTime? LastLoginAt { get; set; }
    }
}

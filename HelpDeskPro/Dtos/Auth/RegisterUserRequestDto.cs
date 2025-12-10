namespace HelpDeskPro.Dtos.Auth
{
    /// <summary>
    /// Datos de entrada para registrar un nuevo usuario.
    /// </summary>
    public class RegisterUserRequestDto
    {
        /// <summary>Email del usuario.</summary>
        public required string Email { get; set; }
        /// <summary>Contraseña del usuario.</summary>
        public required string Password { get; set; }
        /// <summary>Nombre del usuario.</summary>
        public required string FirstName { get; set; }
        /// <summary>Apellidos del Usuario.</summary>
        public required string LastName { get; set; }
        /// <summary>Código de idioma preferido (opcional).</summary>
        public string? LanguageCode { get; set; }
    }
}

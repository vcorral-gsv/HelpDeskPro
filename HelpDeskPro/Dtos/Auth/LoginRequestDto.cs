namespace HelpDeskPro.Dtos.Auth
{
    /// <summary>
    /// Datos de entrada para iniciar sesión.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>Email del usuario.</summary>
        public required string Email { get; set; }
        /// <summary>Contraseña del usuario.</summary>
        public required string Password { get; set; }
        /// <summary>Código de idioma preferido (opcional).</summary>
        public string? LanguageCode { get; set; }
    }
}

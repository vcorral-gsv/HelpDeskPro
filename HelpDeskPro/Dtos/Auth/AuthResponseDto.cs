namespace HelpDeskPro.Dtos.Auth
{
    /// <summary>
    /// Respuesta de autenticación con tokens de acceso y refresco.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>Token de acceso JWT.</summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>Token de refresco.</summary>
        public string RefreshToken { get; set; } = string.Empty;
        /// <summary>Fecha y hora de expiración del token de acceso.</summary>
        public DateTime ExpiresAt { get; set; }
    }
}

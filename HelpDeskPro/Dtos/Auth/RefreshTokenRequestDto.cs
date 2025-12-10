namespace HelpDeskPro.Dtos.Auth
{
    /// <summary>
    /// Solicitud para refrescar el token de acceso.
    /// </summary>
    public class RefreshTokenRequestDto
    {
        /// <summary>Refresh token emitido previamente.</summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}

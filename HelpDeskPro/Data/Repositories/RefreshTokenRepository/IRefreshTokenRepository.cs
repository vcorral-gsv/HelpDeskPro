namespace HelpDeskPro.Data.Repositories.RefreshTokenRepository
{
    /// <summary>
    /// Contrato del repositorio de refresh tokens.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>Obtiene una entidad de refresh token por su valor.</summary>
        Task<Entities.RefreshToken?> GetByTokenAsync(string token);
        /// <summary>Agrega un nuevo refresh token.</summary>
        Task AddAsync(Entities.RefreshToken token);
        /// <summary>Revoca un refresh token existente.</summary>
        Task Revoke(Entities.RefreshToken token);
        /// <summary>Persiste los cambios realizados.</summary>
        Task SaveChangesAsync();
    }
}

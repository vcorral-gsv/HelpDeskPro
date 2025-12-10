namespace HelpDeskPro.Data.Repositories.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {
        Task<Entities.RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(Entities.RefreshToken token);
        Task Revoke(Entities.RefreshToken token);
        Task SaveChangesAsync();
    }
}

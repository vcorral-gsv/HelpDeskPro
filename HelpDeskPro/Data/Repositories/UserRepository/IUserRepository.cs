using HelpDeskPro.Criterias;
using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task SaveChangesAsync();
        Task<PagedResult<User>> GetUsersAsync(
            int pageNumber,
            int pageSize,
            IReadOnlyList<CriteriaBase<User>> criteria
        );
        Task<PagedResult<UsersByRoleGroup>> GetUsersGroupedByRoleAsync(
              int pageNumber,
              int pageSize
        );
        Task<PagedResult<UsersByTeamGroup>> GetUsersGroupedByTeamAsync(
              int pageNumber,
              int pageSize
        );
    }
}

using HelpDeskPro.Criterias;
using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro.Data.Repositories.UserRepository
{
    public class UserRepository(HelpDeskProContext context) : RepositoryBase<User>(context), IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await _set.AddAsync(user);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _set.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _set.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _set
                .Include(u => u.Teams)
                .Include(u => u.ReportedTickets)
                .Include(u => u.AssignedTickets)
                .Include(u => u.TicketComments)
                .Include(u => u.UploadedAttachments)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<User>> GetUsersAsync(
            int pageNumber,
            int pageSize,
            IReadOnlyList<CriteriaBase<User>> criteria
        )
        {
            IQueryable<User> query = _set.AsNoTracking();

            query = ApplyCriteria(query, criteria);

            query = query.OrderByDescending(u => u.CreatedAt);

            return await ToPagedResultAsync(query, pageNumber, pageSize);
        }
        public async Task<PagedResult<UsersByRoleGroup>> GetUsersGroupedByRoleAsync(
            int pageNumber,
            int pageSize
        )
        {
            var query = _set
            .AsNoTracking()
            .GroupBy(u => u.Role);

            var totalGroups = await query.CountAsync();

            var groups = await query
                .OrderByDescending(g => g.Key)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new UsersByRoleGroup
                {
                    RoleName = g.Key.ToString(),
                    Users = g.ToList()
                })
                .ToListAsync();


            return new PagedResult<UsersByRoleGroup>
            {
                Items = groups,
                TotalCount = totalGroups
            };
        }
        public async Task<PagedResult<UsersByTeamGroup>> GetUsersGroupedByTeamAsync(
            int pageNumber,
            int pageSize
        )
        {
            var query = _set
            .AsNoTracking()
            .SelectMany(u => u.Teams, (user, team) => new { user, team })
            .GroupBy(ut => ut.team.Name, ut => ut.user);

            var totalGroups = await query.CountAsync();

            var groups = await query
                .OrderByDescending(g => g.Key)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new UsersByTeamGroup
                {
                    TeamName = g.Key,
                    Users = g.Distinct().ToList()
                })
                .ToListAsync();

            return new PagedResult<UsersByTeamGroup>
            {
                Items = groups,
                TotalCount = totalGroups
            };
        }

    }
}

using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.UserRepository
{
    public sealed class UsersByRoleGroup
    {
        public required string RoleName { get; init; }
        public required IReadOnlyList<User> Users { get; init; }
    }

}

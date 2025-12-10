using HelpDeskPro.Entities;

namespace HelpDeskPro.Data.Repositories.UserRepository
{
    public class UsersByTeamGroup
    {
        public required string TeamName { get; set; }
        public required IReadOnlyList<User> Users { get; set; }
    }
}

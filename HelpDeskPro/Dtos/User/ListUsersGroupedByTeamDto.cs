namespace HelpDeskPro.Dtos.User
{
    public class ListUsersGroupedByTeamDto
    {
        public string TeamName { get; set; } = string.Empty;
        public List<ListUserDto> Users { get; set; } = [];
    }
}

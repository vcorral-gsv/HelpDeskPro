namespace HelpDeskPro.Dtos.User
{
    public class ListRolesWithUsersDto
    {
        public string RoleName { get; set; } = string.Empty;
        public List<ListUserDto> Users { get; set; } = [];
    }
}

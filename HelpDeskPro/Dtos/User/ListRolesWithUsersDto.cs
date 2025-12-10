namespace HelpDeskPro.Dtos.User
{
    /// <summary>
    /// Grupo de usuarios por rol.
    /// </summary>
    public class ListRolesWithUsersDto
    {
        /// <summary>Nombre del rol.</summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>Usuarios pertenecientes al rol.</summary>
        public List<ListUserDto> Users { get; set; } = [];
    }
}

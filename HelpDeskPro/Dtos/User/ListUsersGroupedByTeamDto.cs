namespace HelpDeskPro.Dtos.User
{
    /// <summary>
    /// Grupo de usuarios por equipo.
    /// </summary>
    public class ListUsersGroupedByTeamDto
    {
        /// <summary>Nombre del equipo.</summary>
        public string TeamName { get; set; } = string.Empty;
        /// <summary>Usuarios del equipo.</summary>
        public List<ListUserDto> Users { get; set; } = [];
    }
}

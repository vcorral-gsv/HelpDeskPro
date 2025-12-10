namespace HelpDeskPro.Dtos.User
{
    public class ListUserDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}

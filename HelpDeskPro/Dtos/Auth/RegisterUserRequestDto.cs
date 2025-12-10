namespace HelpDeskPro.Dtos.Auth
{
    public class RegisterUserRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? LanguageCode { get; set; }
    }
}

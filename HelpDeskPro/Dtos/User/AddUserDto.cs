using HelpDeskPro.Consts;
using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Dtos.User
{
    public class AddUserDto : RegisterUserRequestDto
    {
        public Roles Role { get; set; }
        public bool IsActive { get; set;  } = true;
    }
}

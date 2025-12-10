using AutoMapper;
using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Profiles.Auth
{
    /// <summary>
    /// Perfil de AutoMapper para registros de usuario hacia entidad User.
    /// </summary>
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserRequestDto, Entities.User>();
        }
    }
}

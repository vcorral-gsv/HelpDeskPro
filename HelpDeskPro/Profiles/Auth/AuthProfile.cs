using AutoMapper;
using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Profiles.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserRequestDto, Entities.User>();
        }
    }
}

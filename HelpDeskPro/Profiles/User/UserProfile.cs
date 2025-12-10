using AutoMapper;
using HelpDeskPro.Dtos.User;

namespace HelpDeskPro.Profiles.User
{
    /// <summary>
    /// Perfil de AutoMapper para conversión de entidades de usuario a DTOs.
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Aquí van las configuraciones de mapeo
            CreateMap<Entities.User, ListUserDto>();
            CreateMap<Entities.User, DetailUserDto>()
                .ForMember(
                    dest => dest.TeamNames,
                    opt => opt.MapFrom(src => src.Teams.Select(t => t.Name))
                )
                .ForMember(
                    dest => dest.Tickets,
                    opt => opt.MapFrom(src => src.ReportedTickets)
                )
                .ForMember(
                    dest => dest.AssignedTickets,
                    opt => opt.MapFrom(src => src.AssignedTickets)
                )
                .ForMember(
                    dest => dest.Comments,
                    opt => opt.MapFrom(src => src.TicketComments)
                )
                .ForMember(
                    dest => dest.Attachments,
                    opt => opt.MapFrom(src => src.UploadedAttachments)
                );
        }
    }
}

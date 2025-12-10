using AutoMapper;
using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Profiles.Ticket
{
    /// <summary>
    /// Perfil de AutoMapper para entidades de tickets y sus DTOs asociados.
    /// </summary>
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            // Aquí van las configuraciones de mapeo para Ticket
            CreateMap<Entities.Ticket, ListTicketDto>();
            CreateMap<Entities.TicketComment, ListTicketCommentDto>();
            CreateMap<Entities.TicketAttachment, ListTicketAttachmentDto>();
        }
    }
}

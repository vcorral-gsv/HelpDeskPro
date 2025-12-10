using AutoMapper;
using HelpDeskPro.Dtos.Ticket;

namespace HelpDeskPro.Profiles.Ticket
{
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

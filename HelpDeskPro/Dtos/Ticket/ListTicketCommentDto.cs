namespace HelpDeskPro.Dtos.Ticket
{
    public class ListTicketCommentDto
    {
        public int Id { get; set; }
        public required string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

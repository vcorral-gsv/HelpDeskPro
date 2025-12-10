namespace HelpDeskPro.Dtos.Ticket
{
    public class ListTicketAttachmentDto
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}

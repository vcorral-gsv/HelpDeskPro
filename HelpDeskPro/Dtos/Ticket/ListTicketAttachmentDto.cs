namespace HelpDeskPro.Dtos.Ticket
{
    /// <summary>
    /// Información de un adjunto en un ticket.
    /// </summary>
    public class ListTicketAttachmentDto
    {
        /// <summary>Identificador del adjunto.</summary>
        public int Id { get; set; }
        /// <summary>Nombre del archivo.</summary>
        public required string FileName { get; set; }
        /// <summary>Tipo de contenido (MIME).</summary>
        public required string ContentType { get; set; }
        /// <summary>Tamaño del archivo en bytes.</summary>
        public long FileSizeBytes { get; set; }
        /// <summary>Fecha de subida.</summary>
        public DateTime UploadedAt { get; set; }
    }
}

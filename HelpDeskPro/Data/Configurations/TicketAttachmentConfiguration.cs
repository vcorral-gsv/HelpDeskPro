using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro.Data.Configurations
{
    /// <summary>
    /// Configuración EF Core para la entidad TicketAttachment.
    /// </summary>
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder.ToTable("TicketAttachments");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.FileSizeBytes)
                .IsRequired();

            builder.Property(t => t.StorageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.UploadedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

            // Ticket
            builder.HasOne(a => a.Ticket)
                .WithMany(t => t.TicketAttachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // UploadedBy
            builder.HasOne(a => a.UploadedBy)
                .WithMany(u => u.UploadedAttachments)
                .HasForeignKey(a => a.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

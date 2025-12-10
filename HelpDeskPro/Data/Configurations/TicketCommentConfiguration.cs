using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro.Data.Configurations
{
    /// <summary>
    /// Configuración EF Core para la entidad TicketComment.
    /// </summary>
    public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
    {
        public void Configure(EntityTypeBuilder<TicketComment> builder)
        {
            builder.ToTable("TicketComments");

            builder.HasKey(tc => tc.Id);
            builder.Property(tc => tc.Body)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(tc => tc.IsInternal)
                   .HasDefaultValue(false);

            builder.Property(tc => tc.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

            // Ticket
            builder.HasOne(c => c.Ticket)
                .WithMany(t => t.TicketComments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Author
            builder.HasOne(c => c.Author)
                .WithMany(u => u.TicketComments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

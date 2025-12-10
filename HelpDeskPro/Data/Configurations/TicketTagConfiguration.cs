using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro.Data.Configurations
{
    /// <summary>
    /// Configuración EF Core para la entidad TicketTag.
    /// </summary>
    public class TicketTagConfiguration : IEntityTypeConfiguration<TicketTag>
    {
        public void Configure(EntityTypeBuilder<TicketTag> builder)
        {
            builder.ToTable("TicketTags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.Property(t => t.Color)
                .HasMaxLength(7);

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
        }
    }
}

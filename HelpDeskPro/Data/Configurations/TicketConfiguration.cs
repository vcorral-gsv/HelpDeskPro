using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskPro.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(t => t.Code)
                .IsUnique();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

            builder.Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(t => t.Priority).IsRequired();

            // Category (required)
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reporter (required)
            builder.HasOne(t => t.Reporter)
                .WithMany(u => u.ReportedTickets)
                .HasForeignKey(t => t.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Assignee (opcional)
            builder.HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Team (opcional)
            builder.HasOne(t => t.Team)
                .WithMany()
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(t => t.TicketTags)
                .WithMany(tag => tag.Tickets)
                .UsingEntity<Dictionary<string, object>>(
                    "TicketTagAssignment",
                    j => j
                        .HasOne<TicketTag>()
                        .WithMany()
                        .HasForeignKey("TicketTagId")
                        .HasConstraintName("FK_TicketTagAssignment_TicketTag")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Ticket>()
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .HasConstraintName("FK_TicketTagAssignment_Ticket")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("TicketTagAssignments");
                        j.HasKey("TicketId", "TicketTagId");
                    });


        }
    }
}

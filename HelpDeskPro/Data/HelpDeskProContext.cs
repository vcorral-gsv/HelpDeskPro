using HelpDeskPro.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HelpDeskPro.Data
{
    public class HelpDeskProContext(DbContextOptions<HelpDeskProContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();
        public DbSet<TicketComment> TicketComments => Set<TicketComment>();
        public DbSet<TicketAttachment> TicketAttachments => Set<TicketAttachment>();
        public DbSet<TicketTag> TicketTags => Set<TicketTag>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Esta línea aplica TODAS las configuraciones encontradas por assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

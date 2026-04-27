using Microsoft.EntityFrameworkCore;
using ticket_system.Models;

namespace ticket_system.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // EF Core Tables
        public DbSet<User> Users { get; set; }
        
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Column> Columns { get; set; }

        // have to prevent cascading deletes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Creator)
                .WithMany() // can put something here if collection added later
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Assignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = modelBuilder
                .Entity<Column>()
                .HasOne(c => c.Board)
                .WithMany(b => b.Columns)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder
                .Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Column)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);

             // should column delete cascade or be restricted

            _ = modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "Admin" });
        }
    }
}

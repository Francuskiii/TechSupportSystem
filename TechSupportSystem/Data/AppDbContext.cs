using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechSupportSystem.Models;

namespace TechSupportSystem.Data;

public class AppDbContext : DbContext
{

    public AppDbContext() : base() { }
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Ticket entity
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(c => c.TicketId);

            entity.Property(c => c.Description).IsRequired().HasMaxLength(500);

            entity.Property(c => c.Priority).IsRequired().HasMaxLength(20);

            entity.Property(c => c.Status).IsRequired().HasMaxLength(20);

            entity.Property(c => c.CreatedAt).IsRequired();


        });

        //Technician entity
        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(t => t.TechnicianId);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(t => t.Level)
                .IsRequired();
        });


        //Note entity
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(n => n.NoteId);

            entity.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(n => n.CreatedAt)
                .IsRequired();

            // 1-M
            entity
                .HasOne(n => n.Ticket)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // M-M (Ticket <-> Technician)

        modelBuilder.Entity<Ticket>()
           .HasMany(t => t.Technicians)
           .WithMany(t => t.Tickets)
           .UsingEntity(j => j.HasData(
                new { TicketsTicketId = 1, TechniciansTechnicianId = 1 },
                new { TicketsTicketId = 1, TechniciansTechnicianId = 2 },

                new { TicketsTicketId = 2, TechniciansTechnicianId = 2 },

                new { TicketsTicketId = 3, TechniciansTechnicianId = 1 },
                new { TicketsTicketId = 3, TechniciansTechnicianId = 3 }
            ));

        //sample data
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                TicketId = 1,
                Description = "Printer not working",
                Priority = "High",
                Status = "Open",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                TicketId = 2,
                Description = "Email not syncing on mobile",
                Priority = "Medium",
                Status = "Open",
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                TicketId = 3,
                Description = "VPN connection dropping frequently",
                Priority = "High",
                Status = "In Progress",
                CreatedAt = new DateTime(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Technician>().HasData(
            new Technician
            {
                TechnicianId = 1,
                Name = "Alice",
                Level = 2
            },
            new Technician
            {
                TechnicianId = 2,
                Name = "Bob",
                Level = 3
            },
            new Technician
            {
                TechnicianId = 3,
                Name = "Charlie",
                Level = 1
            }
        );

        modelBuilder.Entity<Note>().HasData(
            new Note
            {
                NoteId = 1,
                Content = "Initial diagnostic started",
                CreatedAt = new DateTime(2026, 1, 1, 1, 0, 0, DateTimeKind.Utc),
                TicketId = 1
            },
            new Note
            {
                NoteId = 2,
                Content = "Checked printer queue, jobs stuck",
                CreatedAt = new DateTime(2026, 1, 1, 2, 0, 0, DateTimeKind.Utc),
                TicketId = 1
            },
            new Note
            {
                NoteId = 3,
                Content = "User reconfigured email account",
                CreatedAt = new DateTime(2026, 1, 2, 1, 0, 0, DateTimeKind.Utc),
                TicketId = 2
            },
            new Note
            {
                NoteId = 4,
                Content = "VPN logs show packet loss",
                CreatedAt = new DateTime(2026, 1, 3, 1, 0, 0, DateTimeKind.Utc),
                TicketId = 3
            }
        );


    }
}

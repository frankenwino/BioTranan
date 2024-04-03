using Microsoft.EntityFrameworkCore;
using BioTranan.Core.Entities;

namespace BioTranan.Core.Database;

public class BioTrananDbContext : DbContext
{
    public BioTrananDbContext(DbContextOptions<BioTrananDbContext> options) : base(options) { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>()
            .HasIndex(f => f.ImdbId)
            .IsUnique();

        modelBuilder.Entity<Auditorium>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Reservation>()
            .HasIndex(r => r.ReservationCode)
            .IsUnique();

        modelBuilder.Entity<Reservation>()
            .Property(r => r.ReservationDate)
             .HasColumnType("DATETIME");

        modelBuilder.Entity<Screening>()
            .Property(s => s.Starts)
            .HasColumnType("DATETIME");
    }
}
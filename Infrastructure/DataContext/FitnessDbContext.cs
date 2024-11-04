using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

public class FitnessDBContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<FitnessMember> FitnessMembers { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Workout> Workouts { get; set; }

    public FitnessDBContext(DbContextOptions<FitnessDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessClub.Entities.Configurations.BookingConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
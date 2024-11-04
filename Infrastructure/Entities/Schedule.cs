using System.Formats.Tar;

public class Schedule:BaseEntity
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int WorkoutId { get; set; }
    public Workout Workout { get; set; } = new Workout();
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = new Trainer();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public class Workout:BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = new Trainer();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}

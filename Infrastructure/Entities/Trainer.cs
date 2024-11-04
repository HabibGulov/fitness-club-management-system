public class Trainer : User
{
    public string Specialization { get; set; } = string.Empty;
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}

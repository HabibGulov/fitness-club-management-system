public class NewWorkoutDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int TrainerId { get; set; }
    public decimal Price { get; set; }
}

public class ModifyWorkoutDto : NewWorkoutDto
{

}

public class WorkoutInfoDto:NewWorkoutDto
{
    public int Id { get; set; }
}
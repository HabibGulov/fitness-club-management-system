public class NewScheduleDto
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int WorkoutId { get; set; }
    public int TrainerId { get; set; }
}

public class ScheduleInfoDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int WorkoutId { get; set; }
    public int TrainerId { get; set; }
}

public class ModifyScheduleDto:NewScheduleDto
{

}
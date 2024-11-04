public static class ScheduleMappingExtensions
{
    public static Schedule ToSchedule(this NewScheduleDto newScheduleDto)
    {
        return new Schedule
        {
            Date = newScheduleDto.Date,
            Time = newScheduleDto.Time,
            WorkoutId = newScheduleDto.WorkoutId,
            TrainerId = newScheduleDto.TrainerId
        };
    }

    public static ScheduleInfoDto ToScheduleInfoDto(this Schedule schedule)
    {
        return new ScheduleInfoDto
        {
            Id = schedule.Id,
            Date = schedule.Date,
            Time = schedule.Time,
            WorkoutId = schedule.WorkoutId,
            TrainerId = schedule.TrainerId
        };
    }

    public static Schedule UpdateSchedule(this Schedule schedule, ModifyScheduleDto modifyDto)
    {
        schedule.Date = modifyDto.Date;
        schedule.Time = modifyDto.Time;
        schedule.WorkoutId = modifyDto.WorkoutId;
        schedule.TrainerId = modifyDto.TrainerId;
        schedule.UpdatedAt = DateTime.UtcNow; 
        return schedule;
    }

    public static Schedule DeleteSchedule(this Schedule schedule)
    {
        schedule.IsDeleted = true;
        schedule.DeletedAt = DateTime.UtcNow;
        schedule.UpdatedAt = DateTime.UtcNow;
        schedule.Version += 1;
        return schedule;
    }
}

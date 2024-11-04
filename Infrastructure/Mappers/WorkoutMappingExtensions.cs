public static class WorkoutMappingExtensions
{
    public static Workout ToWorkout(this NewWorkoutDto newWorkoutDto)
    {
        return new Workout
        {
            Name = newWorkoutDto.Name,
            Description = newWorkoutDto.Description,
            Duration = newWorkoutDto.Duration,
            TrainerId = newWorkoutDto.TrainerId,
            Price = newWorkoutDto.Price
        };
    }

    public static WorkoutInfoDto ToWorkoutInfoDto(this Workout workout)
    {
        return new WorkoutInfoDto
        {
            Id = workout.Id,
            Name = workout.Name,
            Description = workout.Description,
            Duration = workout.Duration,
            TrainerId = workout.TrainerId,
            Price = workout.Price
        };
    }

    public static Workout UpdateWorkout(this Workout workout, ModifyWorkoutDto modifyDto)
    {
        workout.Name = modifyDto.Name;
        workout.Description = modifyDto.Description;
        workout.Duration = modifyDto.Duration;
        workout.TrainerId = modifyDto.TrainerId;
        workout.Price = modifyDto.Price;
        workout.UpdatedAt = DateTime.UtcNow;
        return workout;
    }

    public static Workout DeleteWorkout(this Workout workout)
    {
        workout.IsDeleted = true;
        workout.DeletedAt = DateTime.UtcNow;
        workout.UpdatedAt = DateTime.UtcNow;
        workout.Version += 1;
        return workout;
    }
}
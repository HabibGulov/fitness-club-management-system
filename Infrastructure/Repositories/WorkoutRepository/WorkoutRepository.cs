using Microsoft.EntityFrameworkCore;

public class WorkoutRepository(FitnessDBContext context) : IWorkoutRepository
{
    public async Task<BaseResult> CreateWorkout(NewWorkoutDto info)
    {
        bool trainerExists = await context.Trainers.AnyAsync(x => x.Id == info.TrainerId && x.IsDeleted == false);
        if (!trainerExists)
        {
            return BaseResult.Failure(Error.NotFound("Trainer with the specified ID does not exist."));
        }

        bool isAlreadyExist = await context.Workouts.AnyAsync(x => x.Name == info.Name && x.IsDeleted == false);
        if (isAlreadyExist)
            return BaseResult.Failure(Error.AlreadyExist());

        await context.Workouts.AddAsync(info.ToWorkout());
        int result = await context.SaveChangesAsync();

        return result == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteWorkout(int id)
    {
        Workout? workout = await context.Workouts.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (workout is null)
            return BaseResult.Failure(Error.NotFound());

        workout.DeleteWorkout();
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data was not deleted!"))
            : BaseResult.Success();
    }

    public async Task<Result<WorkoutInfoDto>> GetWorkoutById(int id)
    {
        Workout? workout = await context.Workouts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return workout is null
            ? Result<WorkoutInfoDto>.Failure(Error.NotFound())
            : Result<WorkoutInfoDto>.Success(workout.ToWorkoutInfoDto());
    }

    public async Task<Result<PagedResponse<IEnumerable<WorkoutInfoDto>>>> GetWorkouts(WorkoutFilter filter)
    {
        IQueryable<Workout> workouts = context.Workouts.Where(x => x.IsDeleted == false);

        if (!string.IsNullOrEmpty(filter.Name))
            workouts = workouts.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        int count = await workouts.CountAsync();

        IQueryable<WorkoutInfoDto> result = workouts
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToWorkoutInfoDto());

        PagedResponse<IEnumerable<WorkoutInfoDto>> response = PagedResponse<IEnumerable<WorkoutInfoDto>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<WorkoutInfoDto>>>.Success(response);
    }

    public async Task<BaseResult> UpdateWorkout(int id, ModifyWorkoutDto modifyWorkoutDto)
    {
        Workout? workout = await context.Workouts.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (workout is null)
            return BaseResult.Failure(Error.NotFound());

        workout.UpdateWorkout(modifyWorkoutDto);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }
}
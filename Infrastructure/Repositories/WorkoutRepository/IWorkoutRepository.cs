public interface IWorkoutRepository
{
    Task<Result<PagedResponse<IEnumerable<WorkoutInfoDto>>>> GetWorkouts(WorkoutFilter filter);
    Task<Result<WorkoutInfoDto>> GetWorkoutById(int id);
    Task<BaseResult> CreateWorkout(NewWorkoutDto info);
    Task<BaseResult> UpdateWorkout(int id, ModifyWorkoutDto info);
    Task<BaseResult> DeleteWorkout(int id);
}
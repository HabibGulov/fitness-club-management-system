using Microsoft.EntityFrameworkCore;

public class ScheduleRepository(FitnessDBContext context) : IScheduleRepository
{
    public async Task<BaseResult> CreateSchedule(NewScheduleDto info)
    {
        bool isAlreadyExist = await context.Schedules.AnyAsync(x => x.Date.Date == info.Date.Date && x.Time == info.Time && x.IsDeleted == false);
        if (isAlreadyExist)
            return BaseResult.Failure(Error.AlreadyExist());

        await context.Schedules.AddAsync(info.ToSchedule());
        int result = await context.SaveChangesAsync();

        return result == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteSchedule(int id)
    {
        Schedule? schedule = await context.Schedules.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (schedule is null)
            return BaseResult.Failure(Error.NotFound());

        schedule.DeleteSchedule();
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data was not deleted!"))
            : BaseResult.Success();
    }

    public async Task<Result<ScheduleInfoDto>> GetScheduleById(int id)
    {
        Schedule? schedule = await context.Schedules.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return schedule is null
            ? Result<ScheduleInfoDto>.Failure(Error.NotFound())
            : Result<ScheduleInfoDto>.Success(schedule.ToScheduleInfoDto());
    }

    public async Task<Result<PagedResponse<IEnumerable<ScheduleInfoDto>>>> GetSchedules(ScheduleFilter filter)
    {
        IQueryable<Schedule> schedules = context.Schedules.Where(x => x.IsDeleted == false);

        if (filter.WorkoutId > 0)
            schedules = schedules.Where(x => x.WorkoutId == filter.WorkoutId);

        int count = await schedules.CountAsync();

        IQueryable<ScheduleInfoDto> result = schedules
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToScheduleInfoDto());

        PagedResponse<IEnumerable<ScheduleInfoDto>> response = PagedResponse<IEnumerable<ScheduleInfoDto>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<ScheduleInfoDto>>>.Success(response);
    }

    public async Task<BaseResult> UpdateSchedule(int id, ModifyScheduleDto modifyScheduleDto)
    {
        Schedule? schedule = await context.Schedules.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (schedule is null)
            return BaseResult.Failure(Error.NotFound());

        schedule.UpdateSchedule(modifyScheduleDto);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }
}

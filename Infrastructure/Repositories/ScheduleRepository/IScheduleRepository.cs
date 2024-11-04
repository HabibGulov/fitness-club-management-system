public interface IScheduleRepository
{
    Task<BaseResult> CreateSchedule(NewScheduleDto info);
    Task<BaseResult> DeleteSchedule(int id);
    Task<Result<ScheduleInfoDto>> GetScheduleById(int id);
    Task<Result<PagedResponse<IEnumerable<ScheduleInfoDto>>>> GetSchedules(ScheduleFilter filter);
    Task<BaseResult> UpdateSchedule(int id, ModifyScheduleDto modifyScheduleDto);
}
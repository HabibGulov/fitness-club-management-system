public interface ITrainerRepository
{
    Task<Result<PagedResponse<IEnumerable<TrainerInfoDto>>>> GetTrainers(TrainerFilter filter);
    Task<BaseResult> CreateTrainer(NewTrainerDto info);
    Task<BaseResult> UpdateTrainer(int id, ModifyTrainerDto modifyTrainerDto);
    Task<BaseResult> DeleteTrainer(int id);
    Task<Result<TrainerInfoDto>> GetTrainerById(int id);
}
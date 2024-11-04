public interface IFitnessMemberRepository
{
    Task<Result<PagedResponse<IEnumerable<FitnessMemberInfoDto>>>> GetFitnessMembers(FitnessMemberFilter filter);
    Task<BaseResult> CreateFitnessMember(NewFitnessMemberDto info);
    Task<BaseResult> UpdateFitnessMember(int id, ModifyFitnessMemberDto modifyFitnessMemberDto);
    Task<BaseResult> DeleteFitnessMember(int id);
    Task<Result<FitnessMemberInfoDto>> GetFitnessMemberById(int id);
}
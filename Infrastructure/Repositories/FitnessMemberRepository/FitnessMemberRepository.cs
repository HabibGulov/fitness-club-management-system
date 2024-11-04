using Microsoft.EntityFrameworkCore;

public class FitnessMemberRepository(FitnessDBContext context) : IFitnessMemberRepository
{
    public async Task<BaseResult> CreateFitnessMember(NewFitnessMemberDto info)
    {
        bool isAlreadyExist = await context.FitnessMembers.AnyAsync(x => x.Email == info.Email && x.IsDeleted == false);
        if (isAlreadyExist)
            return BaseResult.Failure(Error.AlreadyExist());

        var fitnessMember = new FitnessMember
        {
            FullName = info.FullName,
            Email = info.Email,
            Password = info.Password, 
        };

        await context.FitnessMembers.AddAsync(fitnessMember);
        int result = await context.SaveChangesAsync();

        return result == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteFitnessMember(int id)
    {
        var fitnessMember = await context.FitnessMembers.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (fitnessMember is null)
            return BaseResult.Failure(Error.NotFound());

        fitnessMember.DeleteFitnessMember(); 
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data was not deleted!"))
            : BaseResult.Success();
    }

    public async Task<Result<FitnessMemberInfoDto>> GetFitnessMemberById(int id)
    {
        var fitnessMember = await context.FitnessMembers.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return fitnessMember is null
            ? Result<FitnessMemberInfoDto>.Failure(Error.NotFound())
            : Result<FitnessMemberInfoDto>.Success(new FitnessMemberInfoDto
            {
                Id = fitnessMember.Id,
                FullName = fitnessMember.FullName,
                Email = fitnessMember.Email
            });
    }

    public async Task<Result<PagedResponse<IEnumerable<FitnessMemberInfoDto>>>> GetFitnessMembers(FitnessMemberFilter filter)
    {
        IQueryable<FitnessMember> members = context.FitnessMembers.Where(x => x.IsDeleted == false);

        if (!string.IsNullOrEmpty(filter.FullName))
            members = members.Where(x => x.FullName.Contains(filter.FullName));

        int count = await members.CountAsync();

        IQueryable<FitnessMemberInfoDto> result = members
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => new FitnessMemberInfoDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email
            });

        PagedResponse<IEnumerable<FitnessMemberInfoDto>> response = PagedResponse<IEnumerable<FitnessMemberInfoDto>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<FitnessMemberInfoDto>>>.Success(response);
    }

    public async Task<BaseResult> UpdateFitnessMember(int id, ModifyFitnessMemberDto modifyFitnessMemberDto)
    {
        FitnessMember? fitnessMember = await context.FitnessMembers.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (fitnessMember is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = context.FitnessMembers.Any(x=>x.IsDeleted==false && x.Id!=fitnessMember.Id && x.Email==modifyFitnessMemberDto.Email);
        if(conflict)
            return BaseResult.Failure(Error.Conflict());

        fitnessMember.FullName = modifyFitnessMemberDto.FullName;
        fitnessMember.Email = modifyFitnessMemberDto.Email;
        fitnessMember.Password = modifyFitnessMemberDto.Password; 

        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }
}

using Microsoft.EntityFrameworkCore;

public class TrainerRepository(FitnessDBContext context) : ITrainerRepository
{
    public async Task<BaseResult> CreateTrainer(NewTrainerDto info)
    {
        bool isAlreadyExist = await context.Trainers.AnyAsync(x => x.Email == info.Email && x.IsDeleted == false);
        if (isAlreadyExist)
            return BaseResult.Failure(Error.AlreadyExist());

        await context.Trainers.AddAsync(info.ToTrainer());
        int result = await context.SaveChangesAsync();

        return result == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteTrainer(int id)
    {
        Trainer? trainer = await context.Trainers.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (trainer is null)
            return BaseResult.Failure(Error.NotFound());

        trainer.DeleteTrainer();
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data was not deleted!"))
            : BaseResult.Success();
    }

    public async Task<Result<TrainerInfoDto>> GetTrainerById(int id)
    {
        Trainer? trainer = await context.Trainers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        return trainer is null
            ? Result<TrainerInfoDto>.Failure(Error.NotFound())
            : Result<TrainerInfoDto>.Success(trainer.ToTrainerInfoDto());
    }

    public async Task<Result<PagedResponse<IEnumerable<TrainerInfoDto>>>> GetTrainers(TrainerFilter filter)
    {
        IQueryable<Trainer> trainers = context.Trainers.Where(x => x.IsDeleted == false);

        if (!string.IsNullOrEmpty(filter.FullName))
            trainers = trainers.Where(x => x.FullName.ToLower().Contains(filter.FullName.ToLower()));

        int count = await trainers.CountAsync();

        IQueryable<TrainerInfoDto> result = trainers
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.ToTrainerInfoDto());

        PagedResponse<IEnumerable<TrainerInfoDto>> response = PagedResponse<IEnumerable<TrainerInfoDto>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<TrainerInfoDto>>>.Success(response);
    }

    public async Task<BaseResult> UpdateTrainer(int id, ModifyTrainerDto modifyTrainerDto)
    {
        Trainer? trainer = await context.Trainers.AsTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (trainer is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Trainers.AnyAsync(x => x.Email == modifyTrainerDto.Email && x.Id != id && x.IsDeleted == false);
        if (conflict)
            return BaseResult.Failure(Error.Conflict());

        trainer.UpdateTrainer(modifyTrainerDto);
        int res = await context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }
}

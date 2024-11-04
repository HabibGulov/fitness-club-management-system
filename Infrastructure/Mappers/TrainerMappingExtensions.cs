public static class TrainerMappingExtensions
{
    public static Trainer ToTrainer(this NewTrainerDto newTrainerDto)
    {
        return new Trainer
        {
            FullName = newTrainerDto.FullName,
            Email = newTrainerDto.Email,
            Password = newTrainerDto.Password,
            Specialization = newTrainerDto.Specialization
        };
    }

    public static TrainerInfoDto ToTrainerInfoDto(this Trainer trainer)
    {
        return new TrainerInfoDto
        {
            Id = trainer.Id,
            FullName = trainer.FullName,
            Email = trainer.Email,
            Specialization = trainer.Specialization
        };
    }

    public static Trainer UpdateTrainer(this Trainer trainer, ModifyTrainerDto modifyDto)
    {
        trainer.FullName = modifyDto.FullName;
        trainer.Email = modifyDto.Email;
        trainer.Specialization = modifyDto.Specialization;
        trainer.UpdatedAt = DateTime.UtcNow;
        return trainer;
    }

    public static Trainer DeleteTrainer(this Trainer trainer)
    {
        trainer.IsDeleted = true;
        trainer.DeletedAt = DateTime.UtcNow;
        trainer.UpdatedAt = DateTime.UtcNow;
        trainer.Version += 1;
        return trainer;
    }
}

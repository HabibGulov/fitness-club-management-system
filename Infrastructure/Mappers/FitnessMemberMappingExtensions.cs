public static class FitnessMemberMappingExtensions
{
    public static FitnessMember ToFitnessMember(this NewFitnessMemberDto newMemberDto)
    {
        return new FitnessMember
        {
            FullName = newMemberDto.FullName,
            Email = newMemberDto.Email,
            Password = newMemberDto.Password
        };
    }

    public static FitnessMemberInfoDto ToFitnessMemberInfoDto(this FitnessMember fitnessMember)
    {
        return new FitnessMemberInfoDto
        {
            Id = fitnessMember.Id,
            FullName = fitnessMember.FullName,
            Email = fitnessMember.Email
        };
    }

    public static FitnessMember UpdateFitnessMember(this FitnessMember fitnessMember, ModifyFitnessMemberDto modifyDto)
    {
        fitnessMember.FullName = modifyDto.FullName;
        fitnessMember.Email = modifyDto.Email;
        fitnessMember.Password = modifyDto.Password;
        fitnessMember.UpdatedAt = DateTime.UtcNow;
        return fitnessMember;
    }

    public static FitnessMember DeleteFitnessMember(this FitnessMember fitnessMember)
    {
        fitnessMember.IsDeleted = true;
        fitnessMember.DeletedAt = DateTime.UtcNow;
        fitnessMember.UpdatedAt = DateTime.UtcNow;
        fitnessMember.Version += 1;
        return fitnessMember;
    }
}

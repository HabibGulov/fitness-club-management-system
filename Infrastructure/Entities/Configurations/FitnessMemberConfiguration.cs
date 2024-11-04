using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Entities.Configurations;

public sealed class FitnessMemberConfiguration : IEntityTypeConfiguration<FitnessMember>
{
    public void Configure(EntityTypeBuilder<FitnessMember> builder)
    {
    }
}
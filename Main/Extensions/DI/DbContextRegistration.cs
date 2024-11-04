using Microsoft.EntityFrameworkCore;

namespace BestPracticeExceptionHandler.Extensions.DI;

public static class DbContextRegistration
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<FitnessDBContext>(x =>
        {
            x.UseNpgsql(builder.Configuration["ConnectionString"]);
            x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            x.LogTo(Console.WriteLine);
        });
        return builder;
    }
}
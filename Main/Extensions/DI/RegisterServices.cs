public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IFitnessMemberRepository, FitnessMemberRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        return services;
    }
}
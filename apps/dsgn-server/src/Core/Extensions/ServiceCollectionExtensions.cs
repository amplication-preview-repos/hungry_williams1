using Dsgn.APIs;

namespace Dsgn;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICoursesService, CoursesService>();
        services.AddScoped<IDiscussionsService, DiscussionsService>();
        services.AddScoped<IEnrollmentsService, EnrollmentsService>();
        services.AddScoped<ILessonsService, LessonsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}

namespace Task_EMCO.Server.Extensions;

public static class AddDatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SQLite")));
        return services;
    }
}

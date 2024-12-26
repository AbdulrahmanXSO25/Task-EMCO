namespace Task_EMCO.Server.Extensions;

public static class AddApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IStudentService, StudentService>();
        
        return services;
    }
}
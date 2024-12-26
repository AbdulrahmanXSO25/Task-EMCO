var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDatabase(configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCustomCors(configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    await context.Database.MigrateAsync();

    await SeedApplicationUser.SeedAsync(services, userManager);
    await SeedSubjects.SeedAsync(context);
    await SeedStudents.SeedAsync(context);
}

app.UseCors("CorsPolicy");
app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
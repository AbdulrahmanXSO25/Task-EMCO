namespace Task_EMCO.Server.Data.Seed;

public class SeedSubjects
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!context.Subjects.Any())
        {
             var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seed", "SeedData", "subjects.json");
                
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} could not be found.");
            }

            var jsonData = await File.ReadAllTextAsync(filePath);
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(jsonData);

            if(subjects != null)
            {
                context.Subjects.AddRange(subjects);
                await context.SaveChangesAsync();
            }
        }
    }
}

namespace Task_EMCO.Server.Data.Seed;

public class SeedStudents
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!context.Students.Any())
        {
            var faker = new Faker<Student>()
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.SecondName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.IsMale, f => f.Random.Bool())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber());

            var students = faker.Generate(100);

            var subjects = await context.Subjects.ToListAsync();

            foreach (var student in students)
            {
                var randomSubjects = subjects.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

                student.StudentSubjects = randomSubjects.Select(ss => new StudentSubject
                {
                    Student = student,
                    Subject = ss
                }).ToList();
            }

            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }
    }
}
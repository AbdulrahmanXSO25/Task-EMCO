namespace Task_EMCO.Server.Models;

public class Subject
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<StudentSubject> StudentSubjects { get; set; }
}
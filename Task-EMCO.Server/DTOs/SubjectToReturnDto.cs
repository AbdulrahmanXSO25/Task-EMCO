namespace Task_EMCO.Server.DTOs;

public class SubjectToReturnDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public int StudentsCount { get; set; }

    public SubjectToReturnDto(Subject subject)
    {
        Id = subject.Id;
        Code = subject.Code;
        Name = subject.Name;
        Description = subject.Description;
        StudentsCount = subject.StudentSubjects.Count;
    }
}
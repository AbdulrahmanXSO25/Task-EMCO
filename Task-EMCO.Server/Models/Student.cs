namespace Task_EMCO.Server.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsMale { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<StudentSubject> StudentSubjects { get; set; }
}
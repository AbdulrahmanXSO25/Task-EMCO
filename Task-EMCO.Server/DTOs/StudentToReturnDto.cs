namespace Task_EMCO.Server.DTOs;

public class StudentToReturnDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool IsMale { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public List<StudentSubjectDto> Subjects { get; set; }

    public StudentToReturnDto(Student student)
    {
        Id = student.Id;
        FirstName = student.FirstName;
        SecondName = student.SecondName;
        LastName = student.LastName;
        DateOfBirth = student.DateOfBirth;
        IsMale = student.IsMale;
        Email = student.Email;
        PhoneNumber = student.PhoneNumber;
        Subjects = student.StudentSubjects.Select(ss => new StudentSubjectDto(ss.Subject.Id, ss.Subject.Code)).ToList();
    }

    public class StudentSubjectDto
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public StudentSubjectDto(int id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
namespace Task_EMCO.Server.DTOs;

public class UpdateStudentDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Id is required and must be greater than 0")]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name is required and cannot exceed 50 characters.")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Second Name is required and cannot exceed 50 characters.")]
    public string SecondName { get; set; }

    [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
    public string? LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public bool IsMale { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string Email { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")]
    public string PhoneNumber { get; set; }

    public List<int> SubjectIds { get; set; }

    public Student ToEntity()
    {
        return new Student
        {
            Id = Id,
            FirstName = FirstName,
            SecondName = SecondName,
            LastName = LastName,
            DateOfBirth = DateOfBirth,
            IsMale = IsMale,
            Email = Email,
            PhoneNumber = PhoneNumber,
            StudentSubjects = SubjectIds?.Select(id => new StudentSubject 
            { 
                StudentId = Id,
                SubjectId = id 
            }).ToList() ?? new List<StudentSubject>()
        };
    }
}
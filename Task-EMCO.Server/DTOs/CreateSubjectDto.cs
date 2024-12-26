namespace Task_EMCO.Server.DTOs;

public class CreateSubjectDto
{
    
    [Required]
    [StringLength(8, MinimumLength = 5, ErrorMessage = "Code must be between 5 and 8 characters.")]
    [RegularExpression(@"^[A-Z]{2,5}\d{3}$", ErrorMessage = "Code must be uppercase and the last 3 characters should be numeric.")]
    public string Code { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    public Subject ToEntity()
    {
        return new Subject
        {
            Code = Code,
            Name = Name,
            Description = Description
        };
    }
}
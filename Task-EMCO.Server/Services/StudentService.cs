namespace Task_EMCO.Server.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService
{
    private readonly IStudentRepository _studentRepository = studentRepository;

    public async Task<PaginatedResult<StudentToReturnDto>> GetAllAsync(int pageNumber, int pageSize, string search = null)
    {
        var students = await _studentRepository.GetAllAsync(pageNumber, pageSize, search);
        var totalCount = await _studentRepository.GetTotalCountAsync(search);

        var studentsDto = students.Select(s => new StudentToReturnDto(s)).ToList();

        return new PaginatedResult<StudentToReturnDto>(studentsDto, totalCount, pageNumber, pageSize);
    }

    public async Task<IEnumerable<StudentToReturnDto>> GetAllAsync(string search = null)
    {
        var students = await _studentRepository.GetAllAsync(search);
        return students.Select(s => new StudentToReturnDto(s));
    }

    public async Task<StudentToReturnDto> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return student == null ? null : new StudentToReturnDto(student);
    }

    public async Task CreateAsync(CreateStudentDto dto)
    {
        await _studentRepository.CreateAsync(dto.ToEntity());
    }

    public async Task UpdateAsync(UpdateStudentDto dto)
    {
        await _studentRepository.UpdateAsync(dto.ToEntity());
    }

    public async Task DeleteAsync(DeleteStudentDto dto)
    {
        await _studentRepository.DeleteAsync(dto.Id);
    }
}
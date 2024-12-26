namespace Task_EMCO.Server.Interfaces;

public interface IStudentService
{
    Task<PaginatedResult<StudentToReturnDto>> GetAllAsync(int pageNumber, int pageSize, string search = null);
    Task<IEnumerable<StudentToReturnDto>> GetAllAsync(string search = null);
    Task<StudentToReturnDto> GetByIdAsync(int id);
    Task CreateAsync(CreateStudentDto dto);
    Task UpdateAsync(UpdateStudentDto dto);
    Task DeleteAsync(DeleteStudentDto dto);
}
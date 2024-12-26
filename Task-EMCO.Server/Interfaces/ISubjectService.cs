namespace Task_EMCO.Server.Interfaces;

public interface ISubjectService
{
    Task<PaginatedResult<SubjectToReturnDto>> GetAllAsync(int pageNumber, int pageSize, string search = null);
    Task<IEnumerable<SubjectToReturnDto>> GetAllAsync(string search = null);
    Task<SubjectToReturnDto> GetByIdAsync(int id);
    Task CreateAsync(CreateSubjectDto dto);
    Task UpdateAsync(UpdateSubjectDto dto);
    Task DeleteAsync(DeleteSubjectDto dto);
}
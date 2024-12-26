namespace Task_EMCO.Server.Services;

public class SubjectService(ISubjectRepository subjectRepository) : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;

    public async Task<PaginatedResult<SubjectToReturnDto>> GetAllAsync(int pageNumber, int pageSize, string search = null)
    {
        var subjects = await _subjectRepository.GetAllAsync(pageNumber, pageSize, search);
        var subjectDtos = subjects.Select(subject => new SubjectToReturnDto(subject)).ToList();
        var totalCount = await _subjectRepository.GetTotalCountAsync(search);
        return new PaginatedResult<SubjectToReturnDto>(subjectDtos, totalCount, pageNumber, pageSize);
    }

    public async Task<IEnumerable<SubjectToReturnDto>> GetAllAsync(string search = null)
    {
        var subjects = await _subjectRepository.GetAllAsync(search);
        return subjects.Select(subject => new SubjectToReturnDto(subject)).ToList();
    }

    public async Task<SubjectToReturnDto> GetByIdAsync(int id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        return subject == null ? null : new SubjectToReturnDto(subject);
    }

    public async Task CreateAsync(CreateSubjectDto dto)
    {
        await _subjectRepository.CreateAsync(dto.ToEntity());
    }

    public async Task UpdateAsync(UpdateSubjectDto dto)
    {
        await _subjectRepository.UpdateAsync(dto.ToEntity());
    }

    public async Task DeleteAsync(DeleteSubjectDto dto)
    {
        await _subjectRepository.DeleteAsync(dto.Id);
    }
}
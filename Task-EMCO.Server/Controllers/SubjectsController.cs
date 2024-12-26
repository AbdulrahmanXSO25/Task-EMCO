namespace Task_EMCO.Server.Controllers;

[Authorize]
public class SubjectsController(ISubjectService subjectService) : BaseApiController
{
    private readonly ISubjectService _subjectService = subjectService;

    [HttpGet]
    public async Task<ActionResult<Result<PaginatedResult<SubjectToReturnDto>>>> GetAllAsync([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 6, [FromQuery] string search = null)
    {
        var result = await _subjectService.GetAllAsync(pageNumber, pageSize, search);
        return Ok(Result<PaginatedResult<SubjectToReturnDto>>.SuccessResult(result));
    }

    [HttpGet("all")]
    public async Task<ActionResult<Result<IEnumerable<SubjectToReturnDto>>>> GetAllAsync([FromQuery] string search = null)
    {
        var result = await _subjectService.GetAllAsync(search);
        return Ok(Result<IEnumerable<SubjectToReturnDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<SubjectToReturnDto>>> GetByIdAsync(int id)
    {
        var result = await _subjectService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(Result<SubjectToReturnDto>.ErrorResult("Subject not found"));
        }
        return Ok(Result<SubjectToReturnDto>.SuccessResult(result));
    }

    [HttpPost]
    public async Task<ActionResult<Result<SubjectToReturnDto>>> CreateAsync([FromBody] CreateSubjectDto dto)
    {
        await _subjectService.CreateAsync(dto);
        return Ok(Result<SubjectToReturnDto>.SuccessResult(null));
    }

    [HttpPut]
    public async Task<ActionResult<Result<SubjectToReturnDto>>> UpdateAsync([FromBody] UpdateSubjectDto dto)
    {
        await _subjectService.UpdateAsync(dto);
        return Ok(Result<SubjectToReturnDto>.SuccessResult(null));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<SubjectToReturnDto>>> DeleteAsync(int id)
    {
        var dto = new DeleteSubjectDto(id);
        await _subjectService.DeleteAsync(dto);
        return Ok(Result<SubjectToReturnDto>.SuccessResult(null));
    }
}
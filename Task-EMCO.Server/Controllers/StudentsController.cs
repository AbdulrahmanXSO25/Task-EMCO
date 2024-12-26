namespace Task_EMCO.Server.Controllers;

[Authorize]
public class StudentsController(IStudentService studentService) : BaseApiController
{
    private readonly IStudentService _studentService = studentService;

    [HttpGet]
    public async Task<ActionResult<Result<PaginatedResult<StudentToReturnDto>>>> GetAllAsync([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 6, [FromQuery] string search = null)
    {
        var result = await _studentService.GetAllAsync(pageNumber, pageSize, search);
        return Ok(Result<PaginatedResult<StudentToReturnDto>>.SuccessResult(result));
    }

    [HttpGet("all")]
    public async Task<ActionResult<Result<IEnumerable<StudentToReturnDto>>>> GetAllAsync([FromQuery] string search = null)
    {
        var result = await _studentService.GetAllAsync(search);
        return Ok(Result<IEnumerable<StudentToReturnDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<StudentToReturnDto>>> GetByIdAsync(int id)
    {
        var result = await _studentService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(Result<StudentToReturnDto>.ErrorResult("Student not found"));
        }
        return Ok(Result<StudentToReturnDto>.SuccessResult(result));
    }

    [HttpPost]
    public async Task<ActionResult<Result<StudentToReturnDto>>> CreateAsync([FromBody] CreateStudentDto dto)
    {
        await _studentService.CreateAsync(dto);
        return Ok(Result<StudentToReturnDto>.SuccessResult(null));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<StudentToReturnDto>>> UpdateAsync(int id, [FromBody] UpdateStudentDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(Result<StudentToReturnDto>.ErrorResult("Student ID mismatch"));
        }
        await _studentService.UpdateAsync(dto);
        return Ok(Result<StudentToReturnDto>.SuccessResult(null));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<StudentToReturnDto>>> DeleteAsync(int id)
    {
        var dto = new DeleteStudentDto(id);
        await _studentService.DeleteAsync(dto);
        return Ok(Result<StudentToReturnDto>.SuccessResult(null));
    }
}
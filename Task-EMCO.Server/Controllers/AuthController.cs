using Microsoft.AspNetCore.Identity.Data;

namespace Task_EMCO.Server.Controllers;

public class AuthController : BaseApiController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _jwtService;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized(new { message = "Invalid login attempt." });

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return Unauthorized(new { message = "Invalid login attempt." });

        var token = _jwtService.GenerateToken(new GenerateTokenRequest(user.Id, user.Email));

        var userDto = new UserDto(user.Id, user.Email, user.FirstName, user.LastName);
        var response = new AuthResponseDto(token, userDto);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated." });

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "User not found." });

        var userDto = new UserDto(user.Id, user.Email, user.FirstName, user.LastName);

        return Ok(userDto);
    }
}
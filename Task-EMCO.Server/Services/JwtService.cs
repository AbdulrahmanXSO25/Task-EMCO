namespace Task_EMCO.Server.Services;

public class JwtService : IJwtService
{
    private readonly IConfigurationSection _jwtConfig;
    private readonly SymmetricSecurityKey _key;
    private readonly string _encryptionAlgorithm = SecurityAlgorithms.HmacSha256;

    public JwtService(IConfiguration configuration)
    {
        _jwtConfig = configuration.GetSection("JwtConfig");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig["SecretKey"]));
        _encryptionAlgorithm = SecurityAlgorithms.HmacSha256;
    }

    public string GenerateToken(GenerateTokenRequest request)
    {
        if (!int.TryParse(_jwtConfig["TokenLifetime"], out var tokenLifetime))
        {
            throw new FormatException("TokenLifetime must be a valid integer.");
        }
        var credentials = new SigningCredentials(_key, _encryptionAlgorithm);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, request.Id),
            new Claim(ClaimTypes.Email, request.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtConfig["Issuer"],
            audience: _jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(tokenLifetime),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
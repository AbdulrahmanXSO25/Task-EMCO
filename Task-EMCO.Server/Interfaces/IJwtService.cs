namespace Task_EMCO.Server.Interfaces;

public record GenerateTokenRequest(string Id, string Email);

public interface IJwtService
{
    string GenerateToken(GenerateTokenRequest request);
}
public interface IJwtTokenGenerator
{
    string GenerateToken(string email, string role);
}

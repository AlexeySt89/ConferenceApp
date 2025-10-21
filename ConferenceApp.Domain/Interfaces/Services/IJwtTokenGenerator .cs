namespace ConferenceApp.Domain.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string email, string role);
        string GenerateToken(string email, string id, string role);
    }
}

namespace ConferenceApp.Domain.Interfaces.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string plainPassword);
        bool VerifyPassword(string plainPassword, string passwordHash);
    }
}
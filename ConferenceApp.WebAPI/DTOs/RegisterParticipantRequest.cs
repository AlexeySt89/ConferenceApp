public class RegisterParticipantRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string TitleLecture { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
    public string Section { get; set; } = string.Empty;
}
public class UpdateParticipantRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string TitleLecture { get; set; } = string.Empty;
    public IFormFile? File { get; set; } = null!;
    public string Section { get; set; } = string.Empty;
}
using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ParticipantService _service;
    public AuthController(ParticipantService service) => _service = service;

    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterParticipantRequest request)
    {
        if(request.File == null || request.File.Length == 0)
            return BadRequest("Файл не прикреплён.");

        using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream);

        var dto = new ParticipantDto
        {
            FullName = request.Name,
            Organization = request.Organization,
            TitleLecture = request.TitleLecture,
            Email = request.Email,
            Password = request.Password,
            FileContent = memoryStream.ToArray(),
            FileName = request.File.FileName
        };

        var result = await _service.SubmitAsync(dto);
        if (!result) return BadRequest("Такой email уже используется.");
        return Ok("Участник зарегистрирован.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ParticipantDto dto)
    {
        var isValid = await _service.AuthenticateAsync(dto.Email, dto.Password);
        if (isValid == null)
            return Unauthorized("Invalid credentials");
        else
            return Ok("Authenticated");
    }

    /*[HttpGet("download/{id}")] // для скачивания докладов по id
    public async Task<IActionResult> DownloadReport(Guid id)
    {
        var participant = await _service.GetByIdAsync(id);
        if (participant == null || participant.FileContent == null || participant.FileName == null)
            return NotFound("Файл не найден.");

        return File(participant.FileContent, "application/octet-stream", participant.FileName);
    }
    */

}

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
    public async Task<IActionResult> Register(ParticipantDto dto)
    {
        var result = await _service.SubmitAsync(dto);
        if (!result) return BadRequest("Email already registered");
        return Ok("Registered successfully");
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
}

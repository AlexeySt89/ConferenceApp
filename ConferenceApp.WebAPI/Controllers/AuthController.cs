using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Services;
using ConferenceApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ParticipantService _participantService;
    private readonly JwtService _jwtService; 

    public AuthController(
        ParticipantService service,
        JwtService jwtService) 
    {
        _participantService = service;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterParticipantRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Файл не прикреплён.");

        using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream);

        var dto = new ParticipantDto
        {
            FullName = request.FullName,
            Organization = request.Organization,
            TitleLecture = request.TitleLecture,
            Email = request.Email,
            Password = request.Password,
            ApplicationContent = memoryStream.ToArray(),
            ApplicationName = request.File.FileName,
            IsApproved = null,
            Role = "user",
            Section = request.Section
        };

        var result = await _participantService.SubmitAsync(dto);
        if (!result.Item1) 
            return BadRequest("Такой email уже используется.");

        // Получаем созданного участника для получения роли
        var participant = await _participantService.GetParticipant(dto.Email);

        var token = _jwtService.GenerateToken(
        email: dto.Email,
        userId: result.Item2,
        role: participant?.Role ?? "user" // Передаем роль в генератор токена
    );

        return Ok(new
        {
            Token = token,
            UserId = result.Item2,
            Message = "Участник зарегистрирован."
        });
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var isValid = await _participantService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
        if (isValid.Item1 == string.Empty)
            return Unauthorized("Invalid credentials");

        // Получаем участника для получения роли
        var participant = await _participantService.GetParticipant(isValid.Item1);

        var token = _jwtService.GenerateToken(
            email: isValid.Item1,
            userId: isValid.Item2,
            role: participant?.Role ?? "user" // Передаем роль в генератор токена
        );

        return Ok(new
        {
            Token = token,
            UserId = isValid.Item2,
            Email = isValid.Item1,
            Role = participant?.Role // Возвращаем роль в ответе
        });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userEmailString = User.FindFirst(ClaimTypes.Email)?.Value;

        if (userEmailString == null)
        {
            return Unauthorized("Некорректный идентификатор пользователя");
        }

        var participant = await _participantService.GetParticipant(userEmailString);

        if (participant == null)
        {
            return NotFound("Пользователь не найден");
        }

        var profileDto = new ParticipantDto
        {
            FullName = participant.FullName,
            Organization = participant.Organization,
            Email = participant.Email,
            TitleLecture = participant.TitleLecture,
            ApplicationName = participant.ArticleFileName,
            IsApproved = participant.IsApproved,
            Role = participant.Role 
        };

        return Ok(profileDto);
    }
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] ParticipantDto updateDto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized("Некорректный идентификатор пользователя");
        }

        var updatedParticipant = await _participantService.EditAsync(email, updateDto);

        if (updatedParticipant == null)
        {
            return NotFound("Пользователь не найден");
        }

        var resultDto = new ParticipantDto
        {
            FullName = updatedParticipant.FullName,
            Organization = updatedParticipant.Organization,
            Email = updatedParticipant.Email,
            TitleLecture = updatedParticipant.TitleLecture,
            ApplicationName = updatedParticipant.ArticleFileName
        };

        return Ok(resultDto);
    }

    [Authorize]
    [HttpPut("profile")]
    [Consumes("multipart/form-data")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateParticipantRequest request)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized("Некорректный идентификатор пользователя");
        }

        byte[]? fileContent = null;
        string? fileName = null;

        if (request.File != null && request.File.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);
            fileContent = memoryStream.ToArray();
            fileName = request.File.FileName;
        }

        var dto = new ParticipantDto
        {
            FullName = request.FullName,
            Organization = request.Organization,
            TitleLecture = request.TitleLecture,
            Email = email, 
            ApplicationContent = fileContent,
            ApplicationName = fileName
        };

        var updatedParticipant = await _participantService.EditAsync(email, dto);

        if (updatedParticipant == null)
        {
            return NotFound("Пользователь не найден");
        }

        var resultDto = new ParticipantDto
        {
            FullName = updatedParticipant.FullName,
            Organization = updatedParticipant.Organization,
            Email = updatedParticipant.Email,
            TitleLecture = updatedParticipant.TitleLecture,
            ApplicationName = updatedParticipant.ArticleFileName
        };

        return Ok(resultDto);
    }

    [HttpGet("applications/approved")]
    [Authorize]
    public async Task<IActionResult> GetApprovedApplications()
    {
        // Возвращает только подтвержденные заявки (isApproved = true)
        var applications = await _participantService.GetApprovedPar();
        if (applications == null)
            return NotFound();
        return Ok(applications);
    }

}

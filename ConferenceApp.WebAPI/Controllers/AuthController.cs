using ConferenceApp.Application.Participants.Commands.CreateParticipant;
using ConferenceApp.Application.Participants.Commands.UpdateParticipant;
using ConferenceApp.Application.Participants.Queries.AuthenticateParticipant;
using ConferenceApp.Application.Participants.Queries.GetParticipantByEmail;
using ConferenceApp.WebAPI.DTOs.Requests;
using ConferenceApp.WebAPI.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] RegisterParticipantRequest request)
        {
            try
            {
                var command = new CreateParticipantCommand
                {
                    FullName = request.FullName,
                    Organization = request.Organization,
                    TitleLecture = request.TitleLecture,
                    Email = request.Email,
                    Password = request.Password,
                    Section = request.Section
                };

                var participantId = await _mediator.Send(command);

                var authQuery = new AuthenticateParticipantQuery
                {
                    Email = request.Email,
                    Password = request.Password
                };

                var authResult = await _mediator.Send(authQuery);

                if (!authResult.Success)
                    return BadRequest("Authentication error after registration");

                return Ok(new
                {
                    Token = authResult.Token,
                    UserId = authResult.UserId,
                    Email = authResult.Email,
                    Role = authResult.Role,
                    Message = "The participant has been registered"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for {Email}", request.Email);
                return StatusCode(500, "An error occurred while registering");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            try
            {
                var query = new AuthenticateParticipantQuery
                {
                    Email = loginRequest.Email,
                    Password = loginRequest.Password
                };

                var result = await _mediator.Send(query);

                if (!result.Success)
                    return Unauthorized("Invalid credentials");

                return Ok(new
                {
                    Token = result.Token,
                    UserId = result.UserId,
                    Email = result.Email,
                    Role = result.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical error during login for {Email}", loginRequest.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var email = GetCurrentUserEmail();
                var query = new GetParticipantByEmailQuery { Email = email };
                var participant = await _mediator.Send(query);

                var response = new ProfileResponse
                {
                    Id = participant.Id,
                    FullName = participant.FullName,
                    Organization = participant.Organization,
                    Email = participant.Email,
                    TitleLecture = participant.TitleLecture,
                    Section = participant.Section,
                    IsApproved = participant.IsApproved,
                    HasApplicationFile = participant.HasApplicationFile,
                    HasArticleFile = participant.HasArticleFile
                };

                return Ok(new { Data = response, Message = "Profile retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "get profile");
            }
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateParticipantRequest request)
        {
            try
            {
                var email = GetCurrentUserEmail();
                var getParticipantQuery = new GetParticipantByEmailQuery { Email = email };
                var participant = await _mediator.Send(getParticipantQuery);

                var command = new UpdateParticipantCommand
                {
                    Id = participant.Id,
                    FullName = request.FullName,
                    Organization = request.Organization,
                    TitleLecture = request.TitleLecture,
                    Section = request.Section
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "update profile");
            }
        }
    }
}
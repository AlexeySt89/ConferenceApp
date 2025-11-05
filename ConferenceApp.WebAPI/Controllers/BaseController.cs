using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.WebAPI.Controllers.Attribute;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [ValidateModel]
    public abstract class BaseController : ControllerBase
    {
        protected Guid? GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : null;
        }

        protected string GetCurrentUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value
                ?? throw new UnauthorizedAccessException("User email not found");
        }

        protected IActionResult HandleException(Exception ex, ILogger logger, string operation)
        {
            logger.LogError(ex, "Error during {Operation}", operation);

            return ex switch
            {
                ValidationException validationEx => BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = validationEx.Errors
                }),
                NotFoundException => NotFound(new { Message = ex.Message }),
                BusinessRuleException => Conflict(new { Message = ex.Message }),
                UnauthorizedAccessException => Unauthorized(new { Message = ex.Message }),
                _ => StatusCode(500, new { Message = "An internal server error occurred" })
            };
        }
    }
}

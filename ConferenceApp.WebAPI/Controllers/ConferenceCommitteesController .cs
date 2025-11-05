using ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers;
using ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/conferences/{conferenceId:guid}/committees")]
    public class ConferenceCommitteesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConferenceCommitteesController> _logger;

        public ConferenceCommitteesController(IMediator mediator, ILogger<ConferenceCommitteesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("organizational")]
        public async Task<IActionResult> GetOrganizationalCommittee(Guid conferenceId)
        {
            try
            {
                var query = new GetParOrgCommMembersQuery();
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Organizational committee retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"get organizational committee for conference {conferenceId}");
            }
        }

        [HttpGet("program")]
        public async Task<IActionResult> GetProgramCommittee(Guid conferenceId)
        {
            try
            {
                var query = new GetParProgramCommMembersQuery();
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Program committee retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"get program committee for conference {conferenceId}");
            }
        }
    }
}

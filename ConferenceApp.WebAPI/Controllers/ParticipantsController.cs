using ConferenceApp.Application.Participants.Commands.DeleteParticipant;
using ConferenceApp.Application.Participants.Commands.UpdateParticipantStatus;
using ConferenceApp.Application.Participants.Queries.GetApprovedParticipants;
using ConferenceApp.Application.Participants.Queries.GetParticipants;
using ConferenceApp.WebAPI.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParticipantsController> _logger;

        public ParticipantsController(IMediator mediator, ILogger<ParticipantsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetParticipants()
        {
            try
            {
                var query = new GetParticipantsQuery();
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Participants retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "get participants");
            }
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedParticipants()
        {
            try
            {
                var query = new GetApprovedParticipantsQuery();
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Approved participants retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "get approved participants");
            }
        }

        [HttpPut("{id:guid}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                var command = new UpdateParticipantStatusCommand
                {
                    ParticipantId = id,
                    IsApproved = request.IsApproved,
                    RejectReason = request.RejectReason
                };

                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"update status for participant {id}");
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteParticipant(Guid id)
        {
            try
            {
                var command = new DeleteParticipantCommand { ParticipantId = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"delete participant {id}");
            }
        }
    }
}
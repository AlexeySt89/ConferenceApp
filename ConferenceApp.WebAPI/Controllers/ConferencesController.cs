using ConferenceApp.Application.Conferences.Queries.GetConferenceById;
using ConferenceApp.Application.Conferences.Queries.GetConferences;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferencesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConferencesController> _logger;

        public ConferencesController(IMediator mediator, ILogger<ConferencesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetConferences()
        {
            try
            {
                var query = new GetConferencesQuery();
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Conferences retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "get conferences");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetConference(Guid id)
        {
            try
            {
                var query = new GetConferenceByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(new { Data = result, Message = "Conference retrieved successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"get conference {id}");
            }
        }
    }
}

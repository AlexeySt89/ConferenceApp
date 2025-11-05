using ConferenceApp.Application.Participants.Commands.UploadApplication;
using ConferenceApp.Application.Participants.Commands.UploadArticle;
using ConferenceApp.Application.Participants.Queries.GetParticipantByEmail;
using ConferenceApp.Application.Participants.Queries.GetParticipantFile;
using ConferenceApp.WebAPI.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/materials")]
    [Authorize]
    public class MaterialsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MaterialsController> _logger;

        public MaterialsController(IMediator mediator, ILogger<MaterialsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("application")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadApplication([FromForm] UploadFileRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest(new { Message = "File is required" });

            try
            {
                var email = GetCurrentUserEmail();
                var getParticipantQuery = new GetParticipantByEmailQuery { Email = email };
                var participant = await _mediator.Send(getParticipantQuery);

                using var memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);

                var command = new UploadApplicationCommand
                {
                    ParticipantId = participant.Id,
                    FileContent = memoryStream.ToArray(),
                    FileName = request.File.FileName,
                    ContentType = request.File.ContentType
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Application file uploaded successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "upload application file");
            }
        }

        [HttpPost("article")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadArticle([FromForm] UploadFileRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest(new { Message = "File is required" });

            try
            {
                var email = GetCurrentUserEmail();
                var getParticipantQuery = new GetParticipantByEmailQuery { Email = email };
                var participant = await _mediator.Send(getParticipantQuery);

                using var memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);

                var command = new UploadArticleCommand
                {
                    ParticipantId = participant.Id,
                    FileContent = memoryStream.ToArray(),
                    FileName = request.File.FileName,
                    ContentType = request.File.ContentType
                };

                await _mediator.Send(command);
                return Ok(new { Message = "Article file uploaded successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, "upload article file");
            }
        }

        [HttpGet("participants/{participantId:guid}/application")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DownloadApplication(Guid participantId)
        {
            try
            {
                var query = new GetParticipantFileQuery
                {
                    ParticipantId = participantId,
                    FileType = FileType.Application
                };

                var result = await _mediator.Send(query);

                var safeFileName = new string(result.FileName.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                var downloadFileName = $"Application_{participantId}_{safeFileName}";

                return File(result.FileStream, result.ContentType, downloadFileName);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"download application for participant {participantId}");
            }
        }

        [HttpGet("participants/{participantId:guid}/article")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DownloadArticle(Guid participantId)
        {
            try
            {
                var query = new GetParticipantFileQuery
                {
                    ParticipantId = participantId,
                    FileType = FileType.Article
                };

                var result = await _mediator.Send(query);

                var safeFileName = new string(result.FileName.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                var downloadFileName = $"Article_{participantId}_{safeFileName}";

                return File(result.FileStream, result.ContentType, downloadFileName);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _logger, $"download article for participant {participantId}");
            }
        }
    }
}

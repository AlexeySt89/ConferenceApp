using ConferenceApp.Application.Interfaces;
using ConferenceApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _service = adminService;
            _logger = logger;
        }
        public class UpdateStatusDto
        {
            public bool IsApproved { get; set; }
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications()
        {

            try
            {
                var applications = await _service.GetAllPar();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка заявок");
                return StatusCode(500, "Произошла ошибка при получении списка заявок");
            }
        }

        [HttpPut("applications/{email}/status")]
        public async Task<IActionResult> UpdateParStatus(string email, [FromBody] UpdateStatusDto statusDto)
        {
            try
            {
                var result = await _service.UpdateParStatusAsync(email, statusDto.IsApproved);

                if (!result)
                {
                    return NotFound($"Участник с email {email} не найден");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении статуса участника {email}");
                return StatusCode(500, "Произошла ошибка при обновлении статуса");
            }
        }

        [HttpDelete("applications/{email}")]
        public async Task<IActionResult> RemoveParticipant(string email)
        {
            try
            {
                var isValid = await _service.Remove(email);
                if (!isValid)
                    return NotFound($"Участник с email {email} не найден");

                return Ok($"Участник {email} успешно удален");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении участника {email}");
                return StatusCode(500, "Произошла ошибка при удалении участника");
            }
        }

        [HttpGet("files/{email}/article")]
        public async Task<IActionResult> DownloadArticleFile(string email)
        {
            try
            {
                var (fileStream, fileName, contentType) = await _service.GetParticipantFileAsync(email, "article");

                if (fileStream == null)
                    return NotFound($"Файл статьи для участника {email} не найден");
                // Добавьте в начало методов:
                string safeEmail = new string(email.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                string downloadFileName = $"Заявка участника {safeEmail} {fileName}";

                // Возвращаем файл с новым именем
                return File(fileStream, contentType, downloadFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при скачивании файла статьи для {email}");
                return StatusCode(500, "Произошла ошибка при скачивании файла");
            }
        }

        [HttpGet("files/{email}/application")]
        public async Task<IActionResult> DownloadApplicationFile(string email)
        {
            try
            {
                var (fileStream, fileName, contentType) = await _service.GetParticipantFileAsync(email, "application");

                if (fileStream == null)
                    return NotFound($"Файл заявки для участника {email} не найден");
                // Добавьте в начало методов:
                string safeEmail = new string(email.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                string downloadFileName = $"Заявка участника {safeEmail} {fileName}";

                return File(fileStream, contentType, downloadFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при скачивании файла заявки для {email}");
                return StatusCode(500, "Произошла ошибка при скачивании файла");
            }
        }
    }
}

using ConferenceApp.Application.Interfaces;
using ConferenceApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "admin")] // Добавляем проверку роли здесь
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _service = adminService;
            _logger = logger;
        }
        // DTO для изменения статуса
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
    }
}

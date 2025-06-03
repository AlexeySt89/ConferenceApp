using ConferenceApp.Application.Interfaces;
using ConferenceApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService adminService) => _service = adminService;

        /*[HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            {
                return Unauthorized("Неверные учетные данные");
            }

            var token = _tokenGenerator.GenerateToken(admin.Email, "Admin");
            return Ok(new { token });
        }*/

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var listPar = await _service.GetAllPar();
            if (listPar == null)
                return BadRequest("bazi net");
            else
                return Ok(listPar);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(string email)
        {
            var isValid = await _service.Remove(email);
            if (isValid == false)
                return BadRequest("такого типа нет");
            else
                return Ok("все четко");
        }
    }
}

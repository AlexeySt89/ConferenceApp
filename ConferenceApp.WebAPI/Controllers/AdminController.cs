using ConferenceApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AdminController(AppDbContext context, IJwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

       /* [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            {
                return Unauthorized("Неверные учетные данные");
            }

            var token = _tokenGenerator.GenerateToken(admin.Email, "Admin");
            return Ok(new { token });
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var isValid = await _service.GetParticipants();
            if (isValid == null)
                return BadRequest("bazi net");
            else
                return Ok(isValid);
        }*/
    }
}

using ConferenceApp.Infrastructure.Data;
using ConferenceApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/dev")]
    public class DevController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                var passwordHasher = new PasswordHasher();
                await DatabaseSeeder.SeedAsync(_context, passwordHasher);

                return Ok(new { message = "Test data seeded successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

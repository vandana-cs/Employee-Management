using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.EmployeeLogins
                .FirstOrDefaultAsync(x =>
                    x.Username == dto.Username &&
                    x.Password == dto.Password &&
                    x.IsActive);

            if (user == null)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                });
            }

            return Ok(new LoginResponseDto
            {
                Success = true,
                Username = user.Username,
                Role = "Employee",
                EmployeeId = user.EmployeeId,
                IsActive = user.IsActive,
                Message = "Login successful."
            });
        }
    }
}
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/employee-login")]
    public class EmployeeLoginController : ControllerBase
    {
        private readonly IEmployeeLoginRepository _loginRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeLoginController(
            IEmployeeLoginRepository loginRepository,
            IEmployeeRepository employeeRepository)
        {
            _loginRepository = loginRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLogin(
            EmployeeLoginDto dto)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            if (!employee.IsActive)
            {
                return BadRequest("Employee is inactive.");
            }

            if (await _loginRepository.UsernameExistsAsync(dto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var existingLogin =
                await _loginRepository.GetByEmployeeIdAsync(dto.EmployeeId);

            if (existingLogin != null)
            {
                return BadRequest(
                    "This employee already has a login account.");
            }

            var login = new EmployeeLogin
            {
                EmployeeId = dto.EmployeeId,
                Username = dto.Username,
                Password = dto.Password,
                IsActive = true
            };

            await _loginRepository.AddAsync(login);

            return Ok(new
            {
                message = "Employee login created successfully.",
                employeeId = dto.EmployeeId,
                username = dto.Username
            });
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetLogin(string username)
        {
            var login =
                await _loginRepository.GetByUsernameAsync(username);

            if (login == null)
            {
                return NotFound("Invalid username.");
            }

            return Ok(login);
        }
    }
}
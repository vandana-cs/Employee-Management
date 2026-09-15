using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/leave-types")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypeService _service;

        public LeaveTypesController(ILeaveTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes = await _service.GetAllAsync();

            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leaveType = await _service.GetByIdAsync(id);

            if (leaveType == null)
            {
                return NotFound("Leave type not found.");
            }

            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveType leaveType)
        {
            var result = await _service.AddAsync(leaveType);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.LeaveTypeId },
                result);
        }
    }
}
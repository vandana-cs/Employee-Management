using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveType>> GetAllAsync();

        Task<LeaveType?> GetByIdAsync(int id);

        Task<LeaveType> AddAsync(LeaveType leaveType);
    }
}
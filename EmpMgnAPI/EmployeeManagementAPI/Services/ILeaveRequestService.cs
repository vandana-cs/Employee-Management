using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services
{
    public interface ILeaveRequestService
    {
        Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest);

        Task<LeaveRequest?> GetByIdAsync(int id);

        Task<List<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);

        Task<LeaveRequest> ApproveAsync(int id);

        Task<LeaveRequest> RejectAsync(int id);
    }
}
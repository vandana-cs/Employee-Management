using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface ILeaveRequestRepository
    {
        Task<LeaveRequest> AddAsync(LeaveRequest leaveRequest);

        Task<LeaveRequest?> GetByIdAsync(int id);

        Task<List<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);

        Task<List<LeaveRequest>> GetByEmployeeIdAndDateRangeAsync(
            int employeeId,
            DateTime fromDate,
            DateTime toDate);

        Task<List<LeaveRequest>> GetApprovedByEmployeeAndLeaveTypeAsync(
            int employeeId,
            int leaveTypeId);

        Task UpdateAsync(LeaveRequest leaveRequest);
    }
}
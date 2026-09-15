using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveType>> GetAllAsync();

        Task<LeaveType?> GetByIdAsync(int id);

        Task<LeaveType> AddAsync(LeaveType leaveType);
    }
}
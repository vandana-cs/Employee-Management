using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> AddAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();

            return leaveRequest;
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.LeaveRequestId == id);
        }

        public async Task<List<LeaveRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.AppliedDate)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetByEmployeeIdAndDateRangeAsync(
            int employeeId,
            DateTime fromDate,
            DateTime toDate)
        {
            return await _context.LeaveRequests
                .Where(x =>
                    x.EmployeeId == employeeId &&
                    x.Status != "Rejected" &&
                    x.FromDate <= toDate &&
                    x.ToDate >= fromDate)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetApprovedByEmployeeAndLeaveTypeAsync(
            int employeeId,
            int leaveTypeId)
        {
            return await _context.LeaveRequests
                .Where(x =>
                    x.EmployeeId == employeeId &&
                    x.LeaveTypeId == leaveTypeId &&
                    x.Status == "Approved")
                .ToListAsync();
        }

        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
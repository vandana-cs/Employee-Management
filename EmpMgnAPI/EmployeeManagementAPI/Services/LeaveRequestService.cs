using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveRequestService(
            ILeaveRequestRepository leaveRepository,
            IEmployeeRepository employeeRepository,
            ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(leaveRequest.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            if (!employee.IsActive)
                throw new Exception(
                    "Inactive employee cannot apply for leave.");

            var leaveType =
                await _leaveTypeRepository.GetByIdAsync(leaveRequest.LeaveTypeId);

            if (leaveType == null)
                throw new Exception("Leave type not found.");

            var today = DateTime.Today;

            if (leaveRequest.FromDate.Date < today)
                throw new Exception("Past dates are not allowed.");

            if (leaveRequest.FromDate.Date > leaveRequest.ToDate.Date)
                throw new Exception(
                    "From Date cannot be greater than To Date.");

            var requestedDays =
                (leaveRequest.ToDate.Date -
                 leaveRequest.FromDate.Date).Days + 1;

            if (requestedDays > leaveType.MaximumDays)
                throw new Exception(
                    "Requested leave days exceed the maximum leave balance.");

            var overlapping =
                await _leaveRepository.GetByEmployeeIdAndDateRangeAsync(
                    leaveRequest.EmployeeId,
                    leaveRequest.FromDate.Date,
                    leaveRequest.ToDate.Date);

            if (overlapping.Any())
                throw new Exception(
                    "Employee already has an overlapping leave request.");

            leaveRequest.Status = "Pending";
            leaveRequest.AppliedDate = DateTime.Now;

            return await _leaveRepository.AddAsync(leaveRequest);
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await _leaveRepository.GetByIdAsync(id);
        }

        public async Task<List<LeaveRequest>> GetByEmployeeIdAsync(
            int employeeId)
        {
            return await _leaveRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<LeaveRequest> ApproveAsync(int id)
        {
            var request = await _leaveRepository.GetByIdAsync(id);

            if (request == null)
                throw new Exception("Leave request not found.");

            if (request.Status != "Pending")
                throw new Exception(
                    "Only Pending requests can be approved.");

            var leaveType =
                await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            if (leaveType == null)
                throw new Exception("Leave type not found.");

            var approved =
                await _leaveRepository.GetApprovedByEmployeeAndLeaveTypeAsync(
                    request.EmployeeId,
                    request.LeaveTypeId);

            var alreadyUsed = approved.Sum(x =>
                (x.ToDate.Date - x.FromDate.Date).Days + 1);

            var requestedDays =
                (request.ToDate.Date - request.FromDate.Date).Days + 1;

            if (alreadyUsed + requestedDays > leaveType.MaximumDays)
                throw new Exception(
                    "Leave balance is insufficient.");

            request.Status = "Approved";

            await _leaveRepository.UpdateAsync(request);

            return request;
        }

        public async Task<LeaveRequest> RejectAsync(int id)
        {
            var request = await _leaveRepository.GetByIdAsync(id);

            if (request == null)
                throw new Exception("Leave request not found.");

            if (request.Status != "Pending")
                throw new Exception(
                    "Only Pending requests can be rejected.");

            request.Status = "Rejected";

            await _leaveRepository.UpdateAsync(request);

            return request;
        }
    }
}
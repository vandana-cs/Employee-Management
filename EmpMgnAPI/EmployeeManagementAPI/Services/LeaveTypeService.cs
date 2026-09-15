using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _repository;

        public LeaveTypeService(ILeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaveType>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LeaveType?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<LeaveType> AddAsync(LeaveType leaveType)
        {
            return await _repository.AddAsync(leaveType);
        }
    }
}
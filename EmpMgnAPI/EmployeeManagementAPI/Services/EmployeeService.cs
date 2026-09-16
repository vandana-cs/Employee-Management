using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee> AddAsync(CreateEmployeeDto dto)
        {
            var existingEmployee =
                await _repository.GetByEmailAsync(dto.Email);

            if (existingEmployee != null)
            {
                throw new InvalidOperationException(
                    "Employee with this email already exists.");
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                JoiningDate = dto.JoiningDate,
                IsActive = dto.IsActive
            };

            return await _repository.AddAsync(employee);
        }
    }
}
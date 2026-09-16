using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface IEmployeeLoginRepository
    {
        Task<EmployeeLogin?> GetByUsernameAsync(string username);

        Task<EmployeeLogin?> GetByEmployeeIdAsync(int employeeId);

        Task<bool> UsernameExistsAsync(string username);

        Task<EmployeeLogin> AddAsync(EmployeeLogin login);
    }
}
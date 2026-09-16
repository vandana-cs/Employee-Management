using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeLoginRepository : IEmployeeLoginRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeLoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeLogin?> GetByUsernameAsync(string username)
        {
            return await _context.EmployeeLogins
                .FirstOrDefaultAsync(x =>
                    x.Username.ToLower() == username.ToLower()
                    && x.IsActive);
        }

        public async Task<EmployeeLogin?> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeLogins
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == employeeId);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.EmployeeLogins
                .AnyAsync(x =>
                    x.Username.ToLower() == username.ToLower());
        }

        public async Task<EmployeeLogin> AddAsync(EmployeeLogin login)
        {
            _context.EmployeeLogins.Add(login);

            await _context.SaveChangesAsync();

            return login;
        }
    }
}
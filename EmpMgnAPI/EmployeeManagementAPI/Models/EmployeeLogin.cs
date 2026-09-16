namespace EmployeeManagementAPI.Models
{
    public class EmployeeLogin
    {
        public int EmployeeLoginId { get; set; }

        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
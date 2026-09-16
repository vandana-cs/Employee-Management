namespace EmployeeLeaveManagement.UI.Authentication
{
    public class AuthUser
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
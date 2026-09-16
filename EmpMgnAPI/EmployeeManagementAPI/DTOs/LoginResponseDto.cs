namespace EmployeeManagementAPI.DTOs
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
namespace EmployeeLeaveManagement.UI.Models
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }
    }
}
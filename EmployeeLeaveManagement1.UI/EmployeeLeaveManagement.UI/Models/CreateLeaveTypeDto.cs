namespace EmployeeLeaveManagement.UI.Models
{
    public class CreateLeaveTypeDto
    {
        public string LeaveTypeName { get; set; } = string.Empty;

        public int MaximumDays { get; set; }
    }
}
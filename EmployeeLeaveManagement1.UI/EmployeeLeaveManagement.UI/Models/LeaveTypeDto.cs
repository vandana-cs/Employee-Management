namespace EmployeeLeaveManagement.UI.Models
{
    public class LeaveTypeDto
    {
        public int LeaveTypeId { get; set; }

        public string LeaveTypeName { get; set; } = string.Empty;

        public int MaximumDays { get; set; }
    }
}
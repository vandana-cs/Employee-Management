using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementAPI.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string LeaveTypeName { get; set; } = string.Empty;

        [Required]
        public int MaximumDays { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();
    }
}
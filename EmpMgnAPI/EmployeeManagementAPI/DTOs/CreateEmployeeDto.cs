using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
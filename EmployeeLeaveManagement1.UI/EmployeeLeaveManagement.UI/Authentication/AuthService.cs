namespace EmployeeLeaveManagement.UI.Authentication
{
    public class AuthService
    {
        private readonly List<AuthUser> _users = new()
        {
            new AuthUser
            {
                Username = "admin",
                Password = "admin123",
                Role = "Admin",
                EmployeeId = null,
                IsActive = true
            },

            new AuthUser
            {
                Username = "hr",
                Password = "hr123",
                Role = "HR",
                EmployeeId = null,
                IsActive = true
            },

            new AuthUser
            {
                Username = "vandana",
                Password = "employee123",
                Role = "Employee",
                EmployeeId = 1,
                IsActive = true
            },

            new AuthUser
            {
                Username = "employee2",
                Password = "employee2123",
                Role = "Employee",
                EmployeeId = 2,
                IsActive = true
            },

            new AuthUser
            {
                Username = "employee3",
                Password = "employee3123",
                Role = "Employee",
                EmployeeId = 3,
                IsActive = true
            }
        };

        public AuthUser? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(x =>
                x.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && x.Password == password
                && x.IsActive);

            if (user == null)
                return false;

            CurrentUser = user;
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsInRole(string role)
        {
            return CurrentUser != null &&
                   CurrentUser.Role.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsAdmin() => IsInRole("Admin");

        public bool IsHR() => IsInRole("HR");

        public bool IsEmployee() => IsInRole("Employee");

        public int? GetEmployeeId()
        {
            return CurrentUser?.EmployeeId;
        }
    }
}
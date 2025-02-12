using AccountingTM.Domain;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;

namespace Accounting.Models
{
    public class User : Entity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
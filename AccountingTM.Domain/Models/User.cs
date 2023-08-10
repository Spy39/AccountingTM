using AccountingTM.Domain.Models;

namespace Accounting.Models
{
    public enum Role
    {
        Administrator,
        Moderator,
        User        
    }

    public class User : Entity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}

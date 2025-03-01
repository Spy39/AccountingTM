using Accounting.Models;

namespace AccountingTM.Domain.Models
{
    public class Right : Entity
    {
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

    }
}

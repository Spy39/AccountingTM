using Accounting.Models;
using AccountingTM.Domain.Models.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

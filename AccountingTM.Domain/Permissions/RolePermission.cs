using Accounting.Models;
using AccountingTM.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Permissions
{
    public class RolePermission : Permission
    {
        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
        public int RoleId { get; set; }
    }
}

using AccountingTM.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Permissions
{
    public class RolePermission : Permission
    {
        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
        public int RoleId { get; set; }
    }
}

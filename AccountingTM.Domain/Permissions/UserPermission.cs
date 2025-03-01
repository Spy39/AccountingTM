using Accounting.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Permissions
{
    public class UserPermission : Permission
    {
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}

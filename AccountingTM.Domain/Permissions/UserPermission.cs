using Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Permissions
{
    public class UserPermission : Permission
    {
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}

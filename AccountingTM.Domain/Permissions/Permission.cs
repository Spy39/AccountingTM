using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Permissions
{
    [Table("Permission")]
    public class Permission : Entity
    {
        public string Name { get; set; }
        public bool IsGranted { get; set; }

        [NotMapped]
        public string Icon { get; set; }

        [NotMapped]
        public Permission? Parrent { get; set; }

        [NotMapped]
        public IReadOnlyList<Permission> Children => _children.ToImmutableList();
        [NotMapped]
        private readonly List<Permission> _children = new List<Permission>();

        public Permission(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }
        public Permission()
        {
        }

        public Permission CreateChildPermission(string name, string icon)
        {
            var permission = new Permission(name, icon)
            {
                Parrent = this
            };

            _children.Add(permission);
            return permission;
        }
    }
}

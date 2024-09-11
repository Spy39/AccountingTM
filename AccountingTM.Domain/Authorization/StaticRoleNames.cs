using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Authorization
{
    public static class StaticRoleNames
    {
        public const string Admin = nameof(Admin);
        public const string Moderator = nameof(Moderator);
        public const string User = nameof(User);
    }
}

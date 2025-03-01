using Accounting.Data;
using AccountingTM.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Authorization
{
    public class PermissionChecker
    {
        private readonly DataContext _context;
        private readonly ICurrentUserManager _currentUserManager;

        public PermissionChecker(DataContext context, ICurrentUserManager currentUserManager)
        {
            _context = context;
            _currentUserManager = currentUserManager;
        }

        public async Task<bool> IsGranted(string permissionName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == _currentUserManager.Login);
            if(user == null)
            {
                return false;
            }

            var roleId = user.RoleId;
            var isGrantedForRole = await _context.RolePermissions.AnyAsync(x => x.Name == permissionName && roleId == x.RoleId && x.IsGranted);
            return isGrantedForRole;
        }
    }
}

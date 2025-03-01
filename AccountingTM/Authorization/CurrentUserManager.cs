using AccountingTM.Domain;
using System.Security.Claims;

namespace AccountingTM.Authorization
{
    public class CurrentUserManager : ICurrentUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;



        public CurrentUserManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? Login => _contextAccessor.HttpContext?.User?.Identity?.Name;

    }
}

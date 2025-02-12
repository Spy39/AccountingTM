using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

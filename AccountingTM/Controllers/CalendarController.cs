using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

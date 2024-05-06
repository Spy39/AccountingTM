using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

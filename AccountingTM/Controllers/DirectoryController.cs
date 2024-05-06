using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class DirectoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TypeEquipment()
        {
            return PartialView("_TypeEquipment");
        }
		public IActionResult Brand()
		{
			return PartialView("_Brand");
		}
		public IActionResult Category()
        {
			return PartialView("_Category");
		}
        public IActionResult Employee()
        {
			return PartialView("_Employee");
		}
        public IActionResult Indicators()
        {
			return PartialView("_Indicators");
		}
        public IActionResult Location()
        {
			return PartialView("_Location");
		}
        public IActionResult Roles()
        {
			return PartialView("_Roles");
		}
        public IActionResult Units()
        {
			return PartialView("_Units");
		}
    }
}

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
        public IActionResult Indicator()
        {
			return PartialView("_Indicator");
		}
        public IActionResult Location()
        {
			return PartialView("_Location");
		}
        public IActionResult Unit()
        {
			return PartialView("_Unit");
		}
    }
}

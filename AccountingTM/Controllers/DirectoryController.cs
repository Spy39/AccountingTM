using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
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
		public IActionResult TypeConsumable()
		{
			return PartialView("_TypeConsumable");
		}
		public IActionResult Brand()
		{
			return PartialView("_Brand");
		}
		public IActionResult Category()
        {
			return PartialView("_Category");
		}
		public IActionResult Set()
		{
			return PartialView("_Set");
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

using Accounting.Data;
using Accounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class TechnicalEquipmentController : Controller
    {
        private readonly DataContext _context;

        public TechnicalEquipmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var technicalEquipments = _context.TechnicalEquipment.ToList();
            return View(technicalEquipments);
        }

        [HttpPost]
        public IActionResult Create(TechnicalEquipment input)
        {
            var technicalEquipment = _context.TechnicalEquipment.FirstOrDefault(x => x.Name == input.Name);
            if (technicalEquipment != null)
            {
                return BadRequest();
            }
            _context.TechnicalEquipment.Add(input);
            _context.SaveChanges();
			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technicalEquipment = _context.TechnicalEquipment.Find(id);

            _context.TechnicalEquipment.Remove(technicalEquipment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Find()
        {
            return Ok("Ok");
        }

        [HttpPatch]
        public IActionResult Update()
        {
            return Ok("Ok");
        }
    }
}

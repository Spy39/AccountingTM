using Accounting.Data;
using Accounting.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.ViewModels.TechnicalEquipment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Accounting.Controllers
{
    public class TechnicalEquipmentController : Controller
    {
        private readonly DataContext _context;

        public TechnicalEquipmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                //query = query.Where(x => x.Name.ToLower().Contains(keyword) || x.Model.ToLower().Contains(keyword) ||
                //    x.SerialNumber.ToLower().Contains(keyword));
            }
            var clients = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<TechnicalEquipment>(query.Count(), clients));
        }


        [HttpGet]
        public IActionResult Index()
        {
            var technicalEquipments = _context.TechnicalEquipment.ToList();
            return View(technicalEquipments);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TechnicalEquipment input)
        {
            if(!string.IsNullOrWhiteSpace(input.InventoryNumber))
            {
				if (_context.TechnicalEquipment.Any(x => x.InventoryNumber == input.InventoryNumber))
				{
                    throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
				}
			}
            _context.TechnicalEquipment.Add(input);
            _context.SaveChanges();
            return RedirectToAction("Index");
		}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.TechnicalEquipment.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TechnicalEquipment.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Info(int id)
        {
            TechnicalEquipment technicalEquipment = _context.TechnicalEquipment.Find(id);
            var model = new InfoViewModel
            {
                TechnicalId = id,
                //SerialNumber = technicalEquipment.SerialNumber,
                //InventoryNumber = technicalEquipment.InventoryNumber,
                //EmployeeFio = technicalEquipment.Employee,
                //LocationName = technicalEquipment.Location,
                //Date = technicalEquipment.Date,
                //DateStart = technicalEquipment.DateStart,
                //DateEnd = technicalEquipment.DateEnd,
                //DateGarant = technicalEquipment.DateGarant
            };
            return View(model);
        }

        public IActionResult Set()
		{
			return View();
		}
	}
}

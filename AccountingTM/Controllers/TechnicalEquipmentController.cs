using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.ViewModels.TechnicalEquipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Accounting.Controllers
{
    [Authorize]
    public class TechnicalEquipmentController : Controller
    {
        private readonly DataContext _context;

        public TechnicalEquipmentController(DataContext context)
        {
            _context = context;
        }

        //Технические средства

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment.Include(x => x.Brand).Include(x => x.Type).Include(x => x.Location).Include(x => x.Employee);
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


        //Информация о техническом средстве
        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Info(int id)
        {
            TechnicalEquipment technicalEquipment = _context.TechnicalEquipment.Include(x => x.Brand).Include(x => x.Type).Include(x => x.Location).Include(x => x.Employee).First(x => x.Id == id);
            var model = new InfoViewModel
            {
                TechnicalId = id,
                Brand = technicalEquipment.Brand.Name,
                Model = technicalEquipment.Model,
                TypeEquipment = technicalEquipment.Type.Name,
                SerialNumber = technicalEquipment.SerialNumber,
                InventoryNumber = technicalEquipment.InventoryNumber,
                Employee = technicalEquipment.Employee,
                Location = technicalEquipment.Location,
                Date = technicalEquipment.Date,
                DateStart = technicalEquipment.DateStart,
                DateEnd = technicalEquipment.DateEnd,
                DateGarant = technicalEquipment.DateGarant,
                Status = technicalEquipment.GetStatus(),
            };
            return View(model);
        }

       
        //Характеристики технического средства
		[HttpGet("[controller]/[action]")]
		public IActionResult GetAllCharacteristic([FromQuery] GetAllTechnicalDto input)
		{
			IQueryable<Characteristics> query = _context.Characteristics.Include(x => x.Indicator).Include(x => x.Unit);
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				//query = query.Where(x => x.Name.ToLower().Contains(keyword) || x.Model.ToLower().Contains(keyword) ||
				//	x.SerialNumber.ToLower().Contains(keyword));
			}
			var clients = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

			return Ok(new PagedResultDto<Characteristics>(query.Count(), clients));
		}

        //
  //      public IActionResult Consumable()
  //      {
		//	var consumable = _context.Consumables.ToList();
		//	return View(consumable);
		//}

		public IActionResult Set()
		{
			return View();
		}


	}
}

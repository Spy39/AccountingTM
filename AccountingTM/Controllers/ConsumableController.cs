using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class ConsumableController : Controller
	{
        private readonly DataContext _context;

        public ConsumableController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Consumable> query = _context.Consumables;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                //query = query.Where(x => x.LastName.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<Consumable>(query.Count(), entities));
        }


        [HttpGet]
        public IActionResult Index()
        {
            var consumables = _context.Consumables.ToList();
            return View(consumables);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Consumable input)
        {
            //if (!string.IsNullOrWhiteSpace(input.InventoryNumber))
            //{
            //    if (_context.TechnicalEquipment.Any(x => x.InventoryNumber == input.InventoryNumber))
            //    {
            //        throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
            //    }
            //}
            _context.Consumables.Add(input);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Consumables.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Consumables.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

    }
}

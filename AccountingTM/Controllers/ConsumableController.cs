using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    //Учет расходных материалов
    [Authorize]
    public class ConsumableController : Controller
    {
        private readonly DataContext _context;

        public ConsumableController(DataContext context)
        {
            _context = context;
        }

        //Вывод данных в таблицу
        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<Consumable> query = _context.Consumables.Include(x => x.TypeConsumable).Include(x => x.Brand).Include(x => x.Location).Include(x => x.Unit);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.TypeConsumable.Name.ToLower().Contains(keyword) ||
                                         x.Brand.Name.ToLower().Contains(keyword) ||
                                         x.Model.ToLower().Contains(keyword) ||
                                         x.Location.Name.ToLower().Contains(keyword) ||
                                         x.Unit.Name.ToLower().Contains(keyword) ||
                                         x.Status.ToLower().Contains(keyword));
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
            //В условии сделать совпадение по типу И бренду И модели
            if (!string.IsNullOrWhiteSpace(input.Model))
            {
                if (_context.Consumables.Any(x => x.Model == input.Model))
                {
                    throw new UserFriendlyException("Расходный материал с такой моделью уже существует!");
                }
            }
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

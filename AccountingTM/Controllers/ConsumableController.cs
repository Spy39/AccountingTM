using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
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

        // ✅ Вывод всех расходных материалов (с фильтрацией)
        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Consumable> query = _context.Consumables
                .Include(x => x.TypeConsumable)
                .Include(x => x.Brand)
                .Include(x => x.Location)
                .Include(x => x.Unit)
                .Include(x => x.TechnicalEquipment);

            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x =>
                    x.TypeConsumable.Name.ToLower().Contains(keyword) ||
                    x.Brand.Name.ToLower().Contains(keyword) ||
                    x.Model.ToLower().Contains(keyword) ||
                    x.Location.Name.ToLower().Contains(keyword) ||
                    x.Unit.Name.ToLower().Contains(keyword) ||
                    x.Status.ToLower().Contains(keyword));
            }

            // Фильтр по типу операции
            if (input.InStock.HasValue)
            {
                if (input.InStock.Value)
                {
                    // InStock = true => фильтруем "В наличии"
                    query = query.Where(x => x.Status == "В наличии");
                }
                else
                {
                    // InStock = false => фильтруем "Отсутствует"
                    query = query.Where(x => x.Status == "Отсутствует");
                }
            }

            var totalCount = query.Count();
            var entities = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            return Ok(new PagedResultDto<Consumable>(totalCount, entities));
        }

        // ✅ Страница со списком всех расходных материалов
        [HttpGet]
        public IActionResult Index()
        {
            var consumables = _context.Consumables.ToList();
            return View(consumables);
        }

        // ✅ Создание нового расходного материала
        [HttpPost]
        public IActionResult Create([FromBody] Consumable input)
        {
            if (!string.IsNullOrWhiteSpace(input.Model))
            {
                if (_context.Consumables.Any(x =>
                    x.Model == input.Model &&
                    x.BrandId == input.BrandId &&
                    x.TypeConsumableId == input.TypeConsumableId))
                {
                    throw new UserFriendlyException("Такой расходный материал уже существует!");
                }
            }

            _context.Consumables.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        // ✅ Удаление расходного материала
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
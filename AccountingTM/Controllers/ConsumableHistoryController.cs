using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.Consumable;
using AccountingTM.ViewModels.Consumable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class ConsumableHistoryController : Controller
    {
        private readonly DataContext _context;

        public ConsumableHistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllConsumableHistoryDto input)
        {
            // Загружаем связанные сущности Employee и TechnicalEquipment
            var query = _context.ConsumableHistories
                .Include(x => x.Employee)
                .Include(x => x.TechnicalEquipment)
                .Where(x => x.ConsumableId == input.ConsumableId);

            // Фильтрация по поисковому запросу (комментарий, ФИО сотрудника, модель ТС)
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower(CultureInfo.InvariantCulture);
                query = query.Where(x =>
                    (x.Comment != null && x.Comment.ToLower().Contains(keyword)) ||
                    (x.Employee != null && x.Employee.FullName.ToLower().Contains(keyword)) ||
                    (x.TechnicalEquipment != null && x.TechnicalEquipment.Model.Name.ToLower().Contains(keyword))
                );
            }

            // Фильтр по типу операции
            if (input.IsSupply.HasValue)
            {
                query = query.Where(x => x.IsSupply == input.IsSupply.Value);
            }


            int totalCount = query.Count();
            var entities = query
                .OrderByDescending(x => x.DateOfOperation)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            return Ok(new PagedResultDto<ConsumableHistory>(totalCount, entities));
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            var items = _context.TechnicalEquipment.Select(x => new {
                id = x.Id,
                text = $"{x.Brand.Name} {x.Model} ({x.SerialNumber})"
            }).ToList();

            return Ok(new
            {
                items,
                totalCount = items.Count
            });
        }

        //Редактирование информации о расходном материале
        [HttpGet]
        public IActionResult Get(int id)
        {
            // Загрузка нужных связей (Brand, TypeConsumable, Location, Unit) при необходимости
            var consumable = _context.Consumables
                .Include(c => c.Brand)
                .Include(c => c.TypeConsumable)
                .Include(c => c.Location)
                .Include(c => c.Unit)
                .FirstOrDefault(c => c.Id == id);

            if (consumable == null)
            {
                return NotFound($"Расходный материал с id = {id} не найден.");
            }

            // Формируем DTO или анонимный объект для клиента
            var dto = new
            {
                id = consumable.Id,
                brandId = consumable.BrandId,
                typeConsumableId = consumable.TypeConsumableId,
                locationId = consumable.LocationId,
                unitId = consumable.UnitId,
                model = consumable.Model
            };

            return Ok(dto);
        }


        [HttpPost]
        public IActionResult Update([FromBody] ConsumableUpdateDto input)
        {
            // Ищем расходный материал по ID
            var consumable = _context.Consumables.FirstOrDefault(x => x.Id == input.Id);
            if (consumable == null)
            {
                return NotFound($"Расходный материал с id = {input.Id} не найден.");
            }

            // Обновляем поля
            consumable.BrandId = input.BrandId;
            consumable.TypeConsumableId = input.TypeConsumableId;
            consumable.LocationId = input.LocationId;
            consumable.UnitId = input.UnitId;
            consumable.Model = input.Model;

            _context.Consumables.Update(consumable);
            _context.SaveChanges();

            return Ok(new { message = "Расходный материал успешно обновлён." });
        }


        //Информация о расходном материале
        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Index(int id)
        {
            Consumable consumable = _context.Consumables.Include(x => x.Brand).Include(x => x.TypeConsumable).Include(x => x.Location).Include(x => x.Unit).First(x => x.Id == id);
            var model = new ConsumableViewModel
            {
                ConsumableId = id,
                BrandId = consumable.BrandId,
                TypeConsumableId = consumable.TypeConsumableId,
                UnitId = consumable.UnitId,
                LocationId = consumable.LocationId,
                Brand = consumable.Brand.Name,
                Model = consumable.Model,
                TypeConsumable = consumable.TypeConsumable.Name,
                Location = consumable.Location.Name,
                Unit = consumable.Unit.Name,
                Quantity = consumable.Quantity,
                Status = consumable.Status,
                DateLatestAddition = consumable.DateLatestAddition
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Supply([FromBody] ConsumableHistory input)
        {
            var user = _context.Users.Include(u => u.Employee)
                                     .FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null || user.EmployeeId == null)
                return Unauthorized("Пользователь не найден");

            var consumable = _context.Consumables.FirstOrDefault(c => c.Id == input.ConsumableId);
            if (consumable == null)
                return NotFound($"Расходный материал с id {input.ConsumableId} не найден.");

            if (input.Quantity <= 0)
                return BadRequest("Ошибка: количество должно быть > 0.");

            var history = new ConsumableHistory
            {
                ConsumableId = consumable.Id,
                EmployeeId = user.EmployeeId.Value,
                Quantity = input.Quantity,
                QuantityAfterOperation = consumable.Quantity + input.Quantity,
                DateOfOperation = DateTime.Now,
                IsSupply = true,
                Comment = input.Comment
            };

            consumable.Quantity += input.Quantity;
            consumable.DateLatestAddition = DateTime.Now;

            _context.ConsumableHistories.Add(history);
            _context.SaveChanges(); // ✅ Теперь всё сохранится
            return Ok(new { message = "Расходный материал пополнен." });
        }


        [HttpPost]
        public IActionResult WriteOff([FromBody] ConsumableHistory input)
        {
            // Проверяем, что расходный материал существует
            var consumable = _context.Consumables.Find(input.ConsumableId);
            if (consumable == null)
            {
                return NotFound($"Расходный материал с id {input.ConsumableId} не найден.");
            }

            // Проверка входного количества
            if (input.Quantity <= 0)
            {
                return BadRequest("Количество должно быть больше 0.");
            }

            // Проверяем, что списываемое количество не превышает доступное
            if (consumable.Quantity < input.Quantity)
            {
                return BadRequest($"Введенное количество не может превышать {consumable.Quantity}.");
            }

            // Обновляем расходный материал
            consumable.Quantity -= input.Quantity;

            // Получаем пользователя
            var user = _context.Users.Include(u => u.Employee)
                                     .FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null || user.EmployeeId == null)
            {
                return Unauthorized("Пользователь не найден или отсутствует информация о сотруднике.");
            }

            // Заполняем данные операции
            input.EmployeeId = user.EmployeeId.Value;
            input.DateOfOperation = DateTime.Now;
            input.QuantityAfterOperation = consumable.Quantity;
            input.IsSupply = false;

            // Если указано техническое средство, проверяем его наличие (опционально)
            if (input.TechnicalEquipmentId.HasValue)
            {
                var tech = _context.TechnicalEquipment.Find(input.TechnicalEquipmentId.Value);
                if (tech == null)
                {
                    return BadRequest("Указанное техническое средство не найдено.");
                }
                input.TechnicalEquipment = tech;
            }

            _context.ConsumableHistories.Add(input);
            _context.Consumables.Update(consumable);
            _context.SaveChanges();
            return Ok(new { message = "Расходный материал успешно списан." });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.ConsumableHistories.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.ConsumableHistories.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


    }
}

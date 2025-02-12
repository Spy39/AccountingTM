using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.ViewModels.Consumable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<ConsumableHistory> query = _context.ConsumableHistories.Include(x => x.Employee).Where(x => x.ConsumableId == input.ConsumableId);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                //query = query.Where(x => x.LastName.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<ConsumableHistory>(query.Count(), entities));
        }

        //Редактирование информации о расходном материале
        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Consumables.Find(id);
            if (entity == null)
            {
                throw new Exception($"Расходный материал с id = {id} не найден");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Update([FromBody] Unit input)
        {
            var consumable = _context.Consumables.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (consumable == null)
            {
                throw new Exception($"Единица измерения с id = {input.Id} не найдена");
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                //if (_context.Consumables.Any(x => x.Name == input.Name && x.Id != consumable.Id))
                //{
                //    throw new UserFriendlyException("Единица измерения с таким названием уже существует!");
                //}
            }

            _context.Consumables.Update(consumable);
            _context.SaveChanges();
            return Ok();
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
			var user = _context.Users.First(x => x.Login == User.Identity.Name);
			input.EmployeeId = user.EmployeeId.Value;
			input.DateOfOperation = DateTime.Now;

			var consumable = _context.Consumables.Find(input.ConsumableId);
			consumable.Quantity += input.Quantity;
			consumable.DateLatestAddition = input.DateOfOperation;
			input.QuantityAfterOperation = consumable.Quantity;
			input.IsSupply = true;

			_context.ConsumableHistories.Add(input);
			_context.SaveChanges();
			return Ok();
		}

		[HttpPost]
		public IActionResult WriteOff([FromBody] ConsumableHistory input)
		{
			var consumable = _context.Consumables.Find(input.ConsumableId);
			if (consumable.Quantity < input.Quantity)
			{
				return BadRequest($"Введенное количество не может превышать {consumable.Quantity}");
			}
			consumable.Quantity -= input.Quantity;

			var user = _context.Users.First(x => x.Login == User.Identity.Name);
			input.EmployeeId = user.EmployeeId.Value;
			input.DateOfOperation = DateTime.Now;
			input.QuantityAfterOperation = consumable.Quantity;
			input.IsSupply = false;

			_context.ConsumableHistories.Add(input);
			_context.Consumables.Update(consumable);
			_context.SaveChanges();
			return Ok();
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

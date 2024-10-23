using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
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

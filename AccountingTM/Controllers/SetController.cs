using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.ViewModels.Consumable;
using AccountingTM.ViewModels.Set;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    //Комплекты
    [Authorize]
    public class SetController : Controller
	{
		private readonly DataContext _context;

		public SetController(DataContext context)
		{
			_context = context;
		}

		//Комплект
		[HttpGet("[controller]/[action]")]
		public IActionResult GetAllSet([FromQuery] GetAllTechnicalDto input)
		{
			IQueryable<Set> query = _context.Sets.Include(x => x.Location).Include(x => x.Employee);
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				//query = query.Where(x => x.LastName.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Set>(query.Count(), entities));
		}


		[HttpGet]
		public IActionResult Index()
		{
			var set = _context.Sets.ToList();
			return View(set);
		}

		[HttpPost]
		public IActionResult CreateSet([FromBody] Set input)
		{
			if (!string.IsNullOrWhiteSpace(input.Name))
			{
				if (_context.Sets.Any(x => x.Name == input.Name))
				{
					throw new UserFriendlyException("Комплект с таким названием уже существует!");
				}
			}
			_context.Sets.Add(input);
			_context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteSet(int id)
		{
			var entity = _context.Sets.Find(id);
			if (entity == null)
			{
				return NotFound();
			}

			_context.Sets.Remove(entity);
			_context.SaveChanges();
			return Ok();
		}

		//Состав комплекта
		[HttpGet]
		public IActionResult GetAllCompoundSet([FromQuery] SearchPagedRequestDto input)
		{
			IQueryable<Set> query = _context.Sets;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				query = query.Where(x => x.Name.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Set>(query.Count(), entities));
		}

		//Информация о расходном материале
		[Route("[controller]/{id:int}")]
		[HttpGet]
		public IActionResult Info(int id)
		{
			Set set = _context.Sets.Include(x => x.Location).Include(x => x.Employee).First(x => x.Id == id);
			var model = new SetViewModel
			{
				SetId = id,
				Employee = set.Employee.FullName,
				Location = set.Location.Name,
				Name = set.Name,
				Status = set.StatusSet.ToString()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult CreateCompoundSet([FromBody] Set input)
		{
			if (!string.IsNullOrWhiteSpace(input.Name))
			{
				if (_context.Sets.Any(x => x.Name == input.Name))
				{
					throw new UserFriendlyException("Комплект с таким названием уже существует!");
				}
			}
			_context.Sets.Add(input);
			_context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteCompoundSet(int id)
		{
			var entity = _context.Sets.Find(id);
			if (entity == null)
			{
				return NotFound();
			}

			_context.Sets.Remove(entity);
			_context.SaveChanges();
			return Ok();
		}
	}
}

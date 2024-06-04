using Accounting.Data;
using AccountingTM.Domain.Migrations;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class UnitController : Controller
	{
		private readonly DataContext _context;

		public UnitController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
		{
			IQueryable<Unit> query = _context.Units;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				query = query.Where(x => x.Name.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Unit>(query.Count(), entities));
		}

		[HttpPost]
		public IActionResult Create([FromBody] Unit input)
		{
			if (!string.IsNullOrWhiteSpace(input.Name))
			{
				if (_context.Units.Any(x => x.Name == input.Name))
				{
					throw new UserFriendlyException("Единица измерения с таким названием уже существует!");
				}
			}
			_context.Units.Add(input);
			_context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var entity = _context.Units.Find(id);
			if (entity == null)
			{
				return NotFound();
			}

			_context.Units.Remove(entity);
			_context.SaveChanges();
			return Ok();
		}
	}
}

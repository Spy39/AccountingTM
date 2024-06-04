using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class SetController : Controller
	{
		private readonly DataContext _context;

		public SetController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
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

		[HttpPost]
		public IActionResult Create([FromBody] Set input)
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
		public IActionResult Delete(int id)
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

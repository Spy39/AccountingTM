using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
	public class IndicatorsController : Controller
	{
		private readonly DataContext _context;

		public IndicatorsController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
		{
			IQueryable<Indicator> query = _context.Indicators;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				query = query.Where(x => x.Name.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Indicator>(query.Count(), entities));
		}

		[HttpPost]
		public IActionResult Create([FromBody] Indicator input)
		{
			if (!string.IsNullOrWhiteSpace(input.Name))
			{
				if (_context.Indicators.Any(x => x.Name == input.Name))
				{
					throw new UserFriendlyException("Показатель с таким названием уже существует!");
				}
			}
			_context.Indicators.Add(input);
			_context.SaveChanges();
			return Ok();
		}
	}
}

using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class BrandController : Controller
	{
		private readonly DataContext _context;

		public BrandController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
		{
			IQueryable<Brand> query = _context.Brands;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				query = query.Where(x => x.Name.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Brand>(query.Count(), entities));
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Brands.ToListAsync());
		}


		[HttpPost]
		public IActionResult Create([FromBody] Brand input)
		{
			if (!string.IsNullOrWhiteSpace(input.Name))
			{
				if (_context.Brands.Any(x => x.Name == input.Name))
				{
					throw new UserFriendlyException("Бренд с таким названием уже существует!");
				}
			}
			_context.Brands.Add(input);
			_context.SaveChanges();
			return Ok();
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			Brand list = _context.Brands.Find(id);
			if (list == null)
			{
				return NotFound();
			}
			_context.Brands.Remove(list);
			_context.SaveChanges();
			return Ok();
		}
	}
}

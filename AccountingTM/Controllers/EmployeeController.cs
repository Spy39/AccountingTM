using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly DataContext _context;

		public EmployeeController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
		{
			IQueryable<Employee> query = _context.Employees;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				query = query.Where(x => x.LastName.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<Employee>(query.Count(), entities));
		}

		[HttpPost]
		public IActionResult Create([FromBody] Employee input)
		{
			if (!string.IsNullOrWhiteSpace(input.LastName) && !string.IsNullOrWhiteSpace(input.FirstName) && !string.IsNullOrWhiteSpace(input.FatherName))
			{
				if (_context.Employees.Any(x => x.LastName == input.LastName) && 
					_context.Employees.Any(x => x.FirstName == input.FirstName) && 
					_context.Employees.Any(x => x.FatherName == input.FatherName))
				{
					throw new UserFriendlyException("Данный сотрудник уже существует!");
				}
			}
			_context.Employees.Add(input);
			_context.SaveChanges();
			return Ok();
		}
	}
}

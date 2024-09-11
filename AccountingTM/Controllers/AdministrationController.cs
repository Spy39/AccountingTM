using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {

		//private readonly DataContext _context;

		//public AdministrationController(DataContext context)
		//{
		//	_context = context;
		//}


		//[HttpGet]
		//public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
		//{
		//	IQueryable<Employee> query = _context.Employees;
		//	if (!string.IsNullOrWhiteSpace(input.SearchQuery))
		//	{
		//		var keyword = input.SearchQuery.ToLower();
		//		query = query.Where(x => x.LastName.ToLower().Contains(keyword));
		//	}

		//	var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
		//	return Ok(new PagedResultDto<Employee>(query.Count(), entities));
		//}

		[HttpGet]
		public IActionResult Index()
		{
			//var administrations = _context.Administrations.ToList();
			return View(/*administrations*/);
		}


		//[HttpPost]
		//public IActionResult Create([FromBody] Administration input)
		//{
		//	_context.Applications.Add(input);
		//	_context.SaveChanges();
		//	return RedirectToAction("Index");
		//}

		//[HttpDelete]
		//public IActionResult Delete(int id)
		//{
		//	var entity = _context.Applications.Find(id);
		//	if (entity == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.Applications.Remove(entity);
		//	_context.SaveChanges();
		//	return Ok();
		//}
    }
}

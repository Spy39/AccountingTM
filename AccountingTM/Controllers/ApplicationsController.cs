using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    public class ApplicationsController : Controller
	{
		private readonly DataContext _context;

		public ApplicationsController(DataContext context)
		{
			_context = context;
		}


		[HttpGet("[controller]/[action]")]
		public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
		{
			IQueryable<Application> query = _context.Applications;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				//query = query.Where(x => x.Name.ToLower().Contains(keyword) || x.Model.ToLower().Contains(keyword) ||
				//	x.SerialNumber.ToLower().Contains(keyword));
			}
			var clients = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

			return Ok(new PagedResultDto<Application>(query.Count(), clients));
		}

		[HttpGet]
		public IActionResult Index()
		{
			var applications = _context.Applications.ToList();
			return View(applications);
		}


		[HttpPost]
		public IActionResult Create([FromBody] Application input)
		{
			//if (!string.IsNullOrWhiteSpace(input.Author))
			//{
			//	if (_context.Applications.Any(x => x.InventoryNumber == input.InventoryNumber))
			//	{
			//		throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
			//	}
			//}
			_context.Applications.Add(input);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var entity = _context.Applications.Find(id);
			if (entity == null)
			{
				return NotFound();
			}

			_context.Applications.Remove(entity);
			_context.SaveChanges();
			return Ok();
		}

		[Route("[controller]/{id:int}")]
		[HttpGet]
		public IActionResult Info(int id)
		{
			return View();
		}
	}
}

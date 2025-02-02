﻿using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {

		private readonly DataContext _context;

		public AdministrationController(DataContext context)
		{
			_context = context;
		}


		[HttpGet("[controller]/[action]")]
		public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
		{
			IQueryable<User> query = _context.Users;
			if (!string.IsNullOrWhiteSpace(input.SearchQuery))
			{
				var keyword = input.SearchQuery.ToLower();
				//query = query.Where(x => x.LastName.ToLower().Contains(keyword));
			}

			var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
			return Ok(new PagedResultDto<User>(query.Count(), entities));
		}

		[HttpGet]
		public IActionResult Index()
		{
			var administrations = _context.Users.ToList();
			return View(administrations);
		}


		[HttpPost]
		public IActionResult Create([FromBody] User input)
		{
			_context.Users.Add(input);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var entity = _context.Users.Find(id);
			if (entity == null)
			{
				return NotFound();
			}

			_context.Users.Remove(entity);
			_context.SaveChanges();
			return Ok();
		}
	}
}

﻿using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers.Directorys
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Category> query = _context.Categories;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<Category>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Categories.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Категория с таким названием уже существует!");
                }
            }
            _context.Categories.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Categories.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

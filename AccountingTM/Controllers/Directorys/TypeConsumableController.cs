﻿using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers.Directories
{
    //Типы расходных материалов
    [Authorize]
    public class TypeConsumableController : Controller
    {
        private readonly DataContext _context;

        public TypeConsumableController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<TypeConsumable> query = _context.TypeConsumables;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<TypeConsumable>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.TypeConsumables.Find(id);
            if (entity == null)
            {
                throw new Exception($"Тип расходного материала с id = {id} не найден");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TypeConsumable input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.TypeConsumables.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Расходный материал с таким названием уже существует!");
                }
            }
            _context.TypeConsumables.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.TypeConsumables.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TypeConsumables.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

﻿using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers.Directories
{
    //Показатели
    [Authorize]
    public class IndicatorController : Controller
    {
        private readonly DataContext _context;

        public IndicatorController(DataContext context)
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

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Indicators.Find(id);
            if (entity == null)
            {
                throw new Exception($"Показатель с id = {id} не найден");
            }

            return Ok(entity);
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

        [HttpPost]
        public IActionResult Update([FromBody] Indicator input)
        {
            var indicator = _context.Indicators.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (indicator == null)
            {
                throw new Exception($"Показатель с id = {input.Id} не найден");
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Indicators.Any(x => x.Name == input.Name && x.Id != indicator.Id))
                {
                    throw new UserFriendlyException("Показатель с таким названием уже существует!");
                }
            }

            _context.Indicators.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Indicators.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Indicators.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

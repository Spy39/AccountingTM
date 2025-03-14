﻿using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers.Directories
{
    //Помещения
    [Authorize]
    public class LocationController : Controller
    {
        private readonly DataContext _context;

        public LocationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Location> query = _context.Locations;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Location>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Locations.Find(id);
            if (entity == null)
            {
                throw new Exception($"Помещение с id = {id} не найдено");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Location input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Locations.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Помещение с таким названием уже существует!");
                }
            }
            _context.Locations.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] Location input)
        {
            var location = _context.Locations.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (location == null)
            {
                throw new Exception($"Помещение с id = {input.Id} не найдено");
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Locations.Any(x => x.Name == input.Name && x.Id != location.Id))
                {
                    throw new UserFriendlyException("Помещение с таким названием уже существует!");
                }
            }

            _context.Locations.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Locations.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

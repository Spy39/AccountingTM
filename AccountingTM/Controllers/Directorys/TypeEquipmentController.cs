﻿using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers.Directories
{
    //Типы технических средств
    [Authorize]
    public class TypeEquipmentController : Controller
    {
        private readonly DataContext _context;

        public TypeEquipmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<TypeEquipment> query = _context.TypeEquipments;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<TypeEquipment>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.TypeEquipments.Find(id);
            if (entity == null)
            {
                throw new Exception($"Тип технического средства с id = {id} не найден");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TypeEquipment input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.TypeEquipments.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Тип технического средства с таким названием уже существует!");
                }
            }
            _context.TypeEquipments.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] TypeEquipment input)
        {
            var typeEquipment = _context.TypeEquipments.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (typeEquipment == null)
            {
                throw new Exception($"Тип технического средства с id = {input.Id} не найден");
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.TypeEquipments.Any(x => x.Name == input.Name && x.Id != typeEquipment.Id))
                {
                    throw new UserFriendlyException("Тип технического средства с таким названием уже существует!");
                }
            }

            _context.TypeEquipments.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.TypeEquipments.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TypeEquipments.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

    }
}

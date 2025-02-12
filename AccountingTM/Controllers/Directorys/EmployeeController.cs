using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers.Directories
{
    //Сотрудники
    [Authorize]
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
                query = query.Where(x => x.FirstName.ToLower().Contains(keyword) ||
                                         x.FatherName.ToLower().Contains(keyword) ||
                                         x.LastName.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Employee>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Employees.Find(id);
            if (entity == null)
            {
                throw new Exception($"Сотрудник с id = {id} не найден");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee input)
        {
            if (!string.IsNullOrWhiteSpace(input.LastName) && 
                !string.IsNullOrWhiteSpace(input.FirstName) && 
                !string.IsNullOrWhiteSpace(input.FatherName))
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

        [HttpPost]
        public IActionResult Update([FromBody] Employee input)
        {
            var employee = _context.Employees.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (employee == null)
            {
                throw new Exception($"Сотрудник с id = {input.Id} не найден");
            }

            if (!string.IsNullOrWhiteSpace(input.LastName) && 
                !string.IsNullOrWhiteSpace(input.FirstName) && 
                !string.IsNullOrWhiteSpace(input.FatherName))
            {
                if (_context.Employees.Any(x => x.LastName == input.LastName && x.Id != employee.Id) &&
                    _context.Employees.Any(x => x.FirstName == input.FirstName && x.Id != employee.Id) &&
                    _context.Employees.Any(x => x.FatherName == input.FatherName && x.Id != employee.Id))
                {
                    throw new UserFriendlyException("Данный сотрудник уже существует!");
                }
            }

            _context.Employees.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Employees.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

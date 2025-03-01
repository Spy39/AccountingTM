using Accounting.Data;
using Accounting.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<User> query = _context.Users.Include(x => x.Role);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Login.ToLower().Contains(keyword) ||
                                    x.FirstName.ToLower().Contains(keyword) ||
                                    x.LastName.ToLower().Contains(keyword) ||
                                    x.FatherName.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<User>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Users
                .Include(x => x.Role)
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"Пользователь с id = {id} не найден");
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User input)
        {
            if (!string.IsNullOrWhiteSpace(input.Login))
            {
                if (_context.Users.Any(x => x.Login == input.Login))
                {
                    throw new UserFriendlyException("Пользователь с таким логином уже существует!");
                }
            }
            _context.Users.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] User input)
        {
            var role = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (role == null)
            {
                throw new Exception($"Пользователь с id = {input.Id} не найден");
            }

            if (!string.IsNullOrWhiteSpace(input.Login))
            {
                if (_context.Users.Any(x => x.Login == input.Login && x.Id != role.Id))
                {
                    throw new UserFriendlyException("Пользователь с таким логином уже существует!");
                }
            }

            _context.Users.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            User list = _context.Users.Find(id);
            if (list == null)
            {
                return NotFound();
            }
            _context.Users.Remove(list);
            _context.SaveChanges();
            return Ok();
        }


    }
}
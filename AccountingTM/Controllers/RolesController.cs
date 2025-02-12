using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    public class RolesController : Controller
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Role> query = _context.Roles;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                //query = query.Where(x => x.LastName.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<Role>(query.Count(), entities));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Roles.Find(id);
            if (entity == null)
            {
                throw new Exception($"Роль с id = {id} не найдена");
            }

            return Ok(entity);
        }
    }
}

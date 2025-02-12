using Accounting.Data;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Dto.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AccountingTM.Domain;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace AccountingTM.Controllers.Directories
{
    //Модели
    [Authorize]
    public class ModelController : Controller
    {
        private readonly DataContext _context;

        public ModelController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Model> query = _context.Models;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword) ||
                                         x.Description.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Model>(query.Count(), entities));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var entity = _context.Models.Find(id);
            if(entity == null)
            {
                throw new Exception($"Модель с id = {id} не найдена");
            }

            return Ok(entity);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Models.ToListAsync());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Model input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Models.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Модель с таким названием уже существует!");
                }
            }
            _context.Models.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] Model input)
        {
            var model = _context.Models.AsNoTracking().FirstOrDefault(x => x.Id == input.Id);
            if (model == null)
            {
                throw new Exception($"Модель с id = {input.Id} не найдена");
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Models.Any(x => x.Name == input.Name && x.Id != model.Id))
                {
                    throw new UserFriendlyException("Модель с таким названием уже существует!");
                }
            }

            _context.Models.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Model list = _context.Models.Find(id);
            if (list == null)
            {
                return NotFound();
            }
            _context.Models.Remove(list);
            _context.SaveChanges();
            return Ok();
        }
    }

}

using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.Set;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.Models;
using AccountingTM.ViewModels.Set;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    //Комплекты
    [Authorize]
    public class SetController : Controller
    {
        private readonly DataContext _context;

        public SetController(DataContext context)
        {
            _context = context;
        }

        //Комплект
        [HttpGet("[controller]/[action]")]
        public IActionResult GetAllSet([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<Set> query = _context.Sets.Include(x => x.Location).Include(x => x.Employee);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword) || 
                                         x.StatusSet.ToLower().Contains(keyword) ||
                                         x.Employee.FirstName.ToLower().Contains(keyword) ||
                                         x.Employee.FatherName.ToLower().Contains(keyword) || 
                                         x.Employee.LastName.ToLower().Contains(keyword) ||
                                         x.Location.Name.ToLower().Contains(keyword));
            }

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Set>(query.Count(), entities));
        }


        [HttpGet]
        public IActionResult Index()
        {
            var set = _context.Sets.ToList();
            return View(set);
        }

        [HttpPost]
        public IActionResult CreateSet([FromBody] Set input)
        {
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                if (_context.Sets.Any(x => x.Name == input.Name))
                {
                    throw new UserFriendlyException("Комплект с таким названием уже существует!");
                }
            }
            _context.Sets.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteSet(int id)
        {
            var entity = _context.Sets.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

        //Состав комплекта
        [HttpGet]
        public IActionResult GetAllCompoundSet([FromQuery] GetAllCompoundSetDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment.Include(x => x.Brand).Include(x => x.Type).Include(x => x.Location).Where(x => x.SetId == input.SetId); ;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<TechnicalEquipment>(query.Count(), entities));
        }

        //История изменений
        [HttpGet]
        public IActionResult GetAllHistoryOfChangesSet([FromQuery] GetAllHistoryOfChangesSetDto input)
        {
            IQueryable<SetHistory> query = _context.SetHistories.Where(x => x.SetId == input.SetId);

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return Ok(new PagedResultDto<SetHistory>(query.Count(), entities));
        }

        //Информация о расходном материале
        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Info(int id)
        {
            Set set = _context.Sets.Include(x => x.Location).Include(x => x.Employee).First(x => x.Id == id);
            var model = new SetViewModel
            {
                SetId = id,
                Employee = set?.Employee?.FullName,
                Location = set?.Location?.Name,
                Name = set.Name,
                Status = set.StatusSet.ToString()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCompoundSet([FromBody] CreateCompoundSetDto input)
        {
            foreach (var technicalEquipmentId in input.TechnicalEquipmentIds)
            {
                var technicalEquipment = _context.TechnicalEquipment.Find(technicalEquipmentId);
                technicalEquipment.SetId = input.SetId;
                _context.TechnicalEquipment.Update(technicalEquipment);

            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCompoundSet(int id)
        {
            var entity = _context.Sets.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}

using Accounting.Data;
using AccountingTM.Domain.Models;
using AccountingTM.Dto.Application;
using AccountingTM.Dto.Common;
using AccountingTM.ViewModels.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    //Учет заявок
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly DataContext _context;

        public ApplicationController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllApplicationDto input)
        {
            IQueryable<Application> query = _context.Applications.Include(x => x.Location).Include(x => x.Category).Include(x => x.Employee);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.ApplicationNumber.ToLower().Contains(keyword) ||
                                         //x.DateOfCreation.Contains(keyword) ||
                                         //x.DateOfChange.Contains(keyword) ||
                                         //x.Category.ToLower().Contains(keyword) ||
                                         x.Subject.ToLower().Contains(keyword) ||
                                         //x.Status.ToLower().Contains(keyword) ||
                                         x.Author.ToLower().Contains(keyword) ||
                                         x.Location.Name.ToLower().Contains(keyword)
                                         /*x.Priority.ToLower().Contains(keyword)*/);
            }

            if (input.TechnicalEquipmentId.HasValue)
            {
                query = query.Where(x => x.TechnicalEquipmentId == input.TechnicalEquipmentId);
            }

            var clients = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Application>(query.Count(), clients));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var applications = _context.Applications.ToList();
            return View(applications);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Application input)
        {
            //Генерирование номера заявки
            input.ApplicationNumber = Guid.NewGuid().ToString().Substring(0, 7);


            var now = DateTime.Now;

            input.DateOfCreation = now;
            input.DateOfChange = now;

            //if (!string.IsNullOrWhiteSpace(input.))
            //{
            //	if (_context.TechnicalEquipment.Any(x => x.InventoryNumber == input.InventoryNumber))
            //	{
            //		throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
            //	}
            //}
            _context.Applications.Add(input);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateCompletedWork([FromBody] Application input)
        {
            //Генерирование номера заявки
            input.ApplicationNumber = Guid.NewGuid().ToString().Substring(0, 7);
            var user = _context.Users.First(x => x.Login == User.Identity.Name);
            input.EmployeeId = user.EmployeeId;
            input.DateOfChange = input.DateOfCreation;

            //if (!string.IsNullOrWhiteSpace(input.))
            //{
            //	if (_context.TechnicalEquipment.Any(x => x.InventoryNumber == input.InventoryNumber))
            //	{
            //		throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
            //	}
            //}
            _context.Applications.Add(input);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.Applications.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCompletedWork(int id)
        {
            var entity = _context.CompletedWorks.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.CompletedWorks.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }

        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Info(int id)
        {
            Application application = _context.Applications.Include(x => x.Location).Include(x => x.Category).First(x => x.Id == id);
            var model = new ApplicationViewModel
            {
                ApplicationId = id,
                Location = application.Location.Name,
                Category = application.Category.Name,
                ApplicationNumber = application.ApplicationNumber,
                DateOfCreation = application.DateOfCreation,
                DateOfChange = application.DateOfChange,
                ExpirationDate = application.ExpirationDate,
                Subject = application.Subject,
                Description = application.Description,
                Status = application.GetApplicationStatus(),
                Author = application.Author,
                LastReply = application.LastReply,
                Priority = application.GetApplicationPrioity(),
            };
            return View(model);
        }
    }
}

using Accounting.Data;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class ArchiveController : Controller
    {
        private readonly DataContext _context;

        public ArchiveController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult GetAllSet([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment.Include(x => x.Type).Include(x => x.Brand).Include(x => x.Model).Where(x => x.IsDeleted); ;
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Type.Name.ToLower().Contains(keyword) ||
                                         x.Brand.Name.ToLower().Contains(keyword) ||
                                         x.Model.Name.ToLower().Contains(keyword) ||
                                         x.SerialNumber.ToLower().Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(input.StartDate) && !string.IsNullOrWhiteSpace(input.EndDate))
            {
                DateTime startDate = DateTime.Parse(input.StartDate);
                DateTime endDate = DateTime.Parse(input.EndDate);
                query = query.Where(x => x.DeletedDate >= startDate && x.DeletedDate <= endDate);
            }

            var totalCount = query.Count();

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<TechnicalEquipment>(totalCount, entities));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var archive = _context.TechnicalEquipment.ToList();
            return View(archive);
        }
    }
}

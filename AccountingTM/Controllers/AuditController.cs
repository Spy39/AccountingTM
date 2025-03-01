using Accounting.Data;
using AccountingTM.Dto.Audit;
using AccountingTM.Dto.Common; // Пагинационные DTO: SearchPagedRequestDto, PagedResultDto<T>
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace AccountingTM.Controllers
{
    public class AuditController : Controller
    {
        private readonly DataContext _context;

        public AuditController(DataContext context)
        {
            _context = context;
        }

        // Отображаем страницу аудита
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Audit/GetAll?searchQuery=...&skipCount=0&maxResultCount=10
        [HttpGet]
        public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
        {
            // Получаем IQueryable записей аудита
            var query = _context.AuditEntries.Include(x => x.Properties).AsQueryable();

            // Фильтрация по поисковой строке (например, по имени пользователя или названию таблицы)
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x =>
                    (x.CreatedBy != null && x.CreatedBy.ToLower().Contains(keyword)) ||
                    (x.EntitySetName != null && x.EntitySetName.ToLower().Contains(keyword)));
            }

            if (!string.IsNullOrWhiteSpace(input.StartDate) && !string.IsNullOrWhiteSpace(input.EndDate))
            {
                DateTime startDate = DateTime.Parse(input.StartDate);
                DateTime endDate = DateTime.Parse(input.EndDate);
                query = query.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate);
            }

            var totalCount = query.Count();

            // Выбираем нужную страницу (сортировка по дате создания, убывающе)
            var entities = query.OrderByDescending(x => x.CreatedDate)
                                .Skip(input.SkipCount)
                                .Take(input.MaxResultCount)
                                .ToList();

            // Маппим каждую запись в AuditDto.
            // Предполагаем, что AuditEntry.PrimaryKey – это словарь, и берем первое значение
            var dtos = entities.Select(x => new AuditDto
            {
                Date = x.CreatedDate,
                TableName = x.EntitySetName,
                Action = x.StateName,
                UserName = x.CreatedBy,
                // Преобразуем первичный ключ в int; если невозможно, можно сделать дополнительную проверку
                PrimaryKey = int.Parse(x?.Properties?.Where(x => x.PropertyName == "Id").FirstOrDefault()?.NewValue?.ToString() ?? "0")
            }).ToList();

            return Ok(new PagedResultDto<AuditDto>(totalCount, dtos));
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
    }
}


//public IActionResult AuditIndex()
//{
//    // берем все логи из БД или какую-то выборку
//    var logs = _context.AuditLogs
//                       .Include(x => x.Details)
//                       .OrderByDescending(x => x.Date)
//                       .Take(100) // например, последние 100
//                       .ToList();

//    return View(logs);
//}

//[HttpGet("[controller]/[action]")]
//public IActionResult GetAll([FromQuery] SearchPagedRequestDto input)
//{
//    IQueryable<AuditEntry> query = _context.AuditEntries;
//    //if (!string.IsNullOrWhiteSpace(input.SearchQuery))
//    //{
//    //    var keyword = input.SearchQuery.ToLower();
//    //    query = query.Where(x => x.Name.ToLower().Contains(keyword));
//    //}

//    var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
//    return Ok(new PagedResultDto<AuditEntry>(query.Count(), entities));
//}
//    }
//}

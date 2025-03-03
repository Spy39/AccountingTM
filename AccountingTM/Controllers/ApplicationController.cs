using Accounting.Data;
using AccountingTM.Domain.Enums;
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

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateApplicationDto input)
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(x => x.Id == input.ApplicationId);

            if (application == null)
            {
                return NotFound("Заявка не найдена");
            }

            // Запоминаем старые значения (для истории)
            var oldStatus = application.Status;
            var oldPriority = application.Priority;
            var oldCategoryId = application.CategoryId;

            // Обновляем поля
            application.Status = input.Status;
            application.Priority = input.Priority;
            application.CategoryId = input.CategoryId;
            application.DateOfChange = DateTime.Now;

            // Сохраняем и фиксируем изменения
            await _context.SaveChangesAsync();

            // Записываем историю изменений (примеры)
            var historyEntries = new List<ApplicationHistory>();

            if (oldStatus != application.Status)
            {
                historyEntries.Add(new ApplicationHistory
                {
                    ApplicationId = application.Id,
                    EmployeeId = GetCurrentEmployeeId(), // метод ниже
                    Date = DateTime.Now,
                    TypeOfOperation = "Изменение статуса",
                    Name = $"Статус: {oldStatus} -> {application.Status}"
                });
            }

            if (oldPriority != application.Priority)
            {
                historyEntries.Add(new ApplicationHistory
                {
                    ApplicationId = application.Id,
                    EmployeeId = GetCurrentEmployeeId(),
                    Date = DateTime.Now,
                    TypeOfOperation = "Изменение приоритета",
                    Name = $"Приоритет: {oldPriority} -> {application.Priority}"
                });
            }

            if (oldCategoryId != application.CategoryId)
            {
                historyEntries.Add(new ApplicationHistory
                {
                    ApplicationId = application.Id,
                    EmployeeId = GetCurrentEmployeeId(),
                    Date = DateTime.Now,
                    TypeOfOperation = "Изменение категории",
                    Name = $"Категория: {oldCategoryId} -> {application.CategoryId}"
                });
            }

            if (historyEntries.Any())
            {
                await _context.ApplicationHistories.AddRangeAsync(historyEntries);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AssignToMe([FromBody] int applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);
            if (application == null)
            {
                return NotFound("Заявка не найдена");
            }

            var user = _context.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null)
            {
                return BadRequest("Пользователь не найден");
            }

            application.EmployeeId = user.EmployeeId;
            application.DateOfChange = DateTime.Now;

            // История
            _context.ApplicationHistories.Add(new ApplicationHistory
            {
                ApplicationId = application.Id,
                EmployeeId = user.EmployeeId,
                Date = DateTime.Now,
                TypeOfOperation = "Назначение исполнителя",
                Name = "Заявка назначена на сотрудника " + user.EmployeeId
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsSolved([FromBody] int applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);
            if (application == null)
            {
                return NotFound("Заявка не найдена");
            }

            application.Status = ApplicationStatus.Solved;
            application.DateOfChange = DateTime.Now;
            application.DateOfClosing = DateTime.Now;

            // Сохраняем и добавляем запись в историю
            _context.ApplicationHistories.Add(new ApplicationHistory
            {
                ApplicationId = application.Id,
                EmployeeId = GetCurrentEmployeeId(),
                Date = DateTime.Now,
                TypeOfOperation = "Решение заявки",
                Name = "Заявка помечена как решённая"
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AttachTechnicalEquipment([FromBody] AttachEquipmentDto input)
        {
            var application = await _context.Applications.FindAsync(input.ApplicationId);
            if (application == null)
            {
                return NotFound("Заявка не найдена");
            }
            var te = await _context.TechnicalEquipment.FindAsync(input.TechnicalEquipmentId);
            if (te == null)
            {
                return NotFound("Техническое средство не найдено");
            }

            application.TechnicalEquipmentId = input.TechnicalEquipmentId;
            application.DateOfChange = DateTime.Now;
            await _context.SaveChangesAsync();

            // Запись в историю
            _context.ApplicationHistories.Add(new ApplicationHistory
            {
                ApplicationId = application.Id,
                EmployeeId = GetCurrentEmployeeId(),
                Date = DateTime.Now,
                TypeOfOperation = "Прикрепление к ТС",
                Name = $"Прикреплено ТС: {te.InventoryNumber} (ID={te.Id})"
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        public class AttachEquipmentDto
        {
            public int ApplicationId { get; set; }
            public int TechnicalEquipmentId { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory(int applicationId)
        {
            var history = await _context.ApplicationHistories
                .Include(h => h.Employee)
                .Where(h => h.ApplicationId == applicationId)
                .OrderByDescending(h => h.Date)
                .ToListAsync();

            // Можно сразу вернуть DTO, если нужно
            return Ok(history);
        }



        // Метод, возвращающий Id сотрудника, 
        // который сейчас авторизован (зависит от логики авторизации в вашем проекте).
        private int? GetCurrentEmployeeId()
        {
            // Предположим, что у вас есть связь User -> EmployeeId
            var user = _context.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            return user?.EmployeeId;
        }


        [HttpGet]
        public async Task<IActionResult> GetComments(int applicationId)
        {
            var comments = await _context.CommentsOnTheApplications
                .Include(c => c.Employee) // 🔹 Подключаем данные о пользователе
                .Where(c => c.ApplicationId == applicationId)
                .OrderByDescending(c => c.Date)
                .Select(c => new
                {
                    id = c.Id,
                    text = c.Text,
                    date = c.Date,
                    pathToFile = c.PathToFile,
                    author = c.Employee != null ? $"{c.Employee.LastName} {c.Employee.FirstName}" : "Неизвестный" // 🔹 Отображаем автора
                })
                .ToListAsync();

            return Ok(comments);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.CommentsOnTheApplications.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.CommentsOnTheApplications.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] CommentDto input, IFormFile file)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == User.Identity.Name);

            if (user == null)
            {
                return BadRequest("Пользователь не найден");
            }

            // Проверяем, есть ли у пользователя привязанный EmployeeId
            int? employeeId = user.EmployeeId;

            var comment = new CommentsOnTheApplication
            {
                ApplicationId = input.ApplicationId,
                Text = input.Text,
                Date = DateTime.Now,
                EmployeeId = user.EmployeeId.Value, // 🔹 Указываем сотрудника, если он есть
            };

            // Обрабатываем файл, если он передан
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Сохраняем файл
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                comment.PathToFile = $"/uploads/{fileName}";
            }

            // Добавляем комментарий в БД
            _context.CommentsOnTheApplications.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = comment.Id,
                text = comment.Text,
                date = comment.Date,
                pathToFile = comment.PathToFile,
                author = $"{user.LastName} {user.FirstName}" // 🔹 Возвращаем ФИО автора
            });
        }


    }
}

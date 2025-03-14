﻿using Accounting.Data;
using AccountingTM.Domain;
using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models.Directory;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Dto.Common;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Exceptions;
using AccountingTM.Models;
using AccountingTM.ViewModels.TechnicalEquipment;
using ClosedXML.Excel;
using EnumsNET;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Controllers
{
    //Учет технических средств
    [Authorize]
    public class TechnicalEquipmentController : Controller
    {
        private readonly DataContext _context;
        private readonly ICurrentUserManager _currentUserManager;
        public TechnicalEquipmentController(DataContext context, ICurrentUserManager currentUserManager)
        {
            _context = context;
            _currentUserManager = currentUserManager;
        }

        //Вывод данных в таблицу
        [HttpGet("[controller]/[action]")]
        public IActionResult GetAll([FromQuery] GetAllTechnicalDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment.Include(x => x.Brand).Include(x => x.Type).Include(x => x.Model).Include(x => x.Location).Include(x => x.Employee).Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Type.Name.ToLower().Contains(keyword) ||
                                         x.Brand.Name.ToLower().Contains(keyword) ||
                                         x.Model.Name.ToLower().Contains(keyword) ||
                                         x.SerialNumber.ToLower().Contains(keyword) ||
                                         //x.State.ToLower().Contains(keyword) ||
                                         x.Employee.FirstName.ToLower().Contains(keyword) ||
                                         x.Employee.FatherName.ToLower().Contains(keyword) ||
                                         x.Employee.LastName.ToLower().Contains(keyword) ||
                                         x.Location.Name.ToLower().Contains(keyword));
            }

            if (input.IsWithoutSet)
            {
                query = query.Where(x => x.SetId == null);
            }

            var clients = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<TechnicalEquipment>(query.Count(), clients));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var technicalEquipments = _context.TechnicalEquipment.ToList();
            return View(technicalEquipments);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TechnicalEquipment input)
        {
            if (!string.IsNullOrWhiteSpace(input.InventoryNumber))
            {
                if (_context.TechnicalEquipment.Any(x => x.InventoryNumber == input.InventoryNumber))
                {
                    throw new UserFriendlyException("Техническое средство с таким инвентарным номером уже существует!");
                }
            }
            _context.TechnicalEquipment.Add(input);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _context.TechnicalEquipment.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TechnicalEquipment.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Информация о техническом средстве
        [Route("[controller]/{id:int}")]
        [HttpGet]
        public IActionResult Info(int id)
        {
            TechnicalEquipment technicalEquipment = _context.TechnicalEquipment.Include(x => x.Brand).Include(x => x.Type).Include(x => x.Model).Include(x => x.Location).Include(x => x.Employee).Include(x => x.Set).First(x => x.Id == id);
            var model = new InfoViewModel
            {
                TechnicalId = id,
                Brand = technicalEquipment.Brand.Name,
                Model = technicalEquipment.Model.Name,
                TypeEquipment = technicalEquipment.Type.Name,
                SerialNumber = technicalEquipment.SerialNumber,
                InventoryNumber = technicalEquipment.InventoryNumber,
                Employee = technicalEquipment.Employee,
                Location = technicalEquipment.Location,
                Date = technicalEquipment.Date,
                DateStart = technicalEquipment.DateStart,
                WorkTimeAvg = technicalEquipment.WorkTimeAvg,
                DateGarant = technicalEquipment.DateGarant,
                Status = technicalEquipment.GetStatus(),
                Set = technicalEquipment.Set,
                IsDeleted = technicalEquipment.IsDeleted
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult GetAllModel([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<TechnicalEquipment> query = _context.TechnicalEquipment.Include(x => x.Model);
            if (!string.IsNullOrWhiteSpace(input.SearchQuery))
            {
                var keyword = input.SearchQuery.ToLower();
                query = query.Where(x => x.Model.Name.ToLower().Contains(keyword));
            }

            var entities = query.Select(x => x.Model.Name).Distinct().ToList();
            return Ok(new PagedResultDto<string>(entities.Count(), entities));
        }

        

        [HttpPost]
        public IActionResult WriteOff(int id)
        {
            var equipment = _context.TechnicalEquipment.Find(id);
            if (equipment == null)
                return NotFound();

            equipment.IsDeleted = true;
            equipment.DeletedDate = DateTime.UtcNow;

            var employee = _context.Users.FirstOrDefault(x => x.Login == _currentUserManager.Login);
            if (employee == null)
                return Unauthorized();

            _context.TechnicalEquipmentHistories.Add(new TechnicalEquipmentHistory
            {
                TechnicalEquipmentId = id,
                EmployeeId = employee.Id,
                Date = DateTime.UtcNow,
                TypeOfOperation = "Списание",
                Name = "ТС списано"
            });
            _context.TechnicalEquipment.Update(equipment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не выбран или пустой");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1); // Первый лист Excel
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Пропуск заголовка

                        foreach (var row in rows)
                        {
                            // Считывание данных из строки
                            string typeName = row.Cell(1).GetValue<string>();
                            string brandName = row.Cell(2).GetValue<string>();
                            string responsibleName = row.Cell(4).GetValue<string>();
                            string locationName = row.Cell(5).GetValue<string>();

                            // Проверка или добавление типа
                            var type = _context.TypeEquipments.FirstOrDefault(t => t.Name == typeName);
                            if (type == null)
                            {
                                type = new TypeEquipment { Name = typeName };
                                _context.TypeEquipments.Add(type);
                                await _context.SaveChangesAsync();
                            }

                            // Проверка или добавление бренда
                            var brand = _context.Brands.FirstOrDefault(b => b.Name == brandName);
                            if (brand == null)
                            {
                                brand = new Brand { Name = brandName };
                                _context.Brands.Add(brand);
                                await _context.SaveChangesAsync();
                            }

                            var fio = responsibleName.Split(' ');
                            // Проверка или добавление ответственного
                            var responsible = _context.Employees.FirstOrDefault(r => r.FirstName == fio[1] && r.LastName == fio[0] && r.FatherName == fio[2]);
                            if (responsible == null)
                            {
                                responsible = new Employee { FirstName = fio[1], LastName = fio[0], FatherName = fio[2] };
                                _context.Employees.Add(responsible);
                                await _context.SaveChangesAsync();
                            }

                            // Проверка или добавление местоположения
                            var location = _context.Locations.FirstOrDefault(l => l.Name == locationName);
                            if (location == null)
                            {
                                location = new Location { Name = locationName };
                                _context.Locations.Add(location);
                                await _context.SaveChangesAsync();
                            }

                            var garantTime = row.Cell(11).GetValue<int>();
                            var state = row.Cell(12).GetValue<string>();
                            var modelName = row.Cell(3).GetValue<string>();
                            var model = _context.Models.FirstOrDefault(x => x.Name == modelName);
                            if(model == null)
                            {
                                model = new Model { Name = modelName };
                                _context.Models.Add(model);
                                _context.SaveChanges();
                            }
                            // Добавление записи в таблицу Учет ТС
                            var device = new TechnicalEquipment
                            {
                                TypeId = type.Id,
                                BrandId = brand.Id,
                                EmployeeId = responsible.Id,
                                LocationId = location.Id,
                                ModelId = model.Id,
                                SerialNumber = row.Cell(6).GetValue<string>(),
                                InventoryNumber = row.Cell(7).GetValue<string>(),
                                Date = row.Cell(8).GetValue<DateTime>(),
                                DateStart = row.Cell(9).GetValue<DateTime>(),
                                WorkTimeAvg = row.Cell(10).GetValue<int>(),
                                State = Enums.Parse<ConditionEquipment>(state, true, EnumFormat.Description)
                            };
                            device.DateGarant = device.DateStart.Value.AddMonths(garantTime);
                            _context.TechnicalEquipment.Add(device);
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                return Ok("Файл успешно загружен и данные добавлены в базу.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }
}

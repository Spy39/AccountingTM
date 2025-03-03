using Accounting.Data;
using AccountingTM.Domain.Enums;
using AccountingTM.Dto.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    //Статистика
    [Authorize]
    public class StatisticController : Controller
    {
        private readonly DataContext _context;

        public StatisticController(DataContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View(new StatisticDto()); // Изначально загружаем пустую модель, а данные будем запрашивать через AJAX
        //}

        //[HttpPost]
        //public IActionResult GetStatistics(DateTime? startDate, DateTime? endDate)
        //{
        //    var technicalEquipments = _context.TechnicalEquipment.AsQueryable();
        //    var applications = _context.Applications.AsQueryable();
        //    var consumables = _context.Consumables.AsQueryable();
        //    var consumableHistories = _context.ConsumableHistories.AsQueryable();

        //    if (startDate.HasValue && endDate.HasValue)
        //    {
        //        technicalEquipments = technicalEquipments.Where(x => x.DateStart >= startDate && x.DateStart <= endDate);
        //        applications = applications.Where(x => x.DateOfCreation >= startDate && x.DateOfCreation <= endDate);
        //        consumableHistories = consumableHistories.Where(x => x.DateOfOperation >= startDate && x.DateOfOperation <= endDate);
        //    }

        //    var result = new StatisticDto
        //    {
        //        TechicalEquipment = new TechicalEquipmentStatisticDto
        //        {
        //            TotalCount = technicalEquipments.Count(),
        //            FaultCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Faulty),
        //            ActiveCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Serviceable),
        //            WorkableCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Efficient),
        //            InoperableCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Inoperative),
        //            WrittenOffCount = technicalEquipments.Count(x => x.IsDeleted)
        //        },
        //        Application = new ApplicationStatisticDto
        //        {
        //            TotalCount = applications.Count(),
        //            SolvedCount = applications.Count(x => x.Status == ApplicationStatus.Solved),
        //            NewCount = applications.Count(x => x.Status == ApplicationStatus.New),
        //            InProgressRequestsCount = applications.Count(x => x.Status == ApplicationStatus.InProgress),
        //            TransferredCount = applications.Count(x => x.Status == ApplicationStatus.Transferred),
        //            SuspendedCount = applications.Count(x => x.Status == ApplicationStatus.Suspended)
        //        },
        //        Consumable = new ConsumableStatisticDto
        //        {
        //            TotalCount = consumables.Count(),
        //            InStockCount = consumables.Count(x => x.Status == "В наличии"),
        //            LowStockCount = consumables.Count(x => x.Status == "Малый запас"),
        //            OutOfStockCount = consumables.Count(x => x.Status == "Отсутствует"),
        //            AvgUsagePerMonth = consumableHistories.Any()
        //                ? consumableHistories
        //                    .GroupBy(x => x.DateOfOperation.Value.Month)
        //                    .Average(g => g.Sum(x => x.Quantity))
        //                : 0,
        //            MostUsedConsumable = consumableHistories
        //                .GroupBy(x => x.Consumable.Model)
        //                .OrderByDescending(g => g.Count())
        //                .Select(g => g.Key)
        //                .FirstOrDefault() ?? "Не определено"
        //        }
        //    };

        //    return Json(result);
        //}

        public IActionResult Index()
        {
            var technicalEquipments = _context.TechnicalEquipment.ToList();
            var applications = _context.Applications.ToList();
            var consumables = _context.Consumables.ToList();
            //try
            //{
            //    Console.WriteLine(_context.ConsumableHistories
            //        .Where(x => x.DateOfOperation.HasValue && x.DateOfOperation >= DateTime.Now.AddMonths(-6))
            //        .GroupBy(x => x.DateOfOperation.Value.Month)
            //        .Select(g => new { Month = g.Key, TotalQuantity = g.Sum(x => x.Quantity) }) // Вычисляем сумму
            //        .AsEnumerable() // Переключаем на клиентскую обработку
            //        .Select(g => g.TotalQuantity)
            //        .DefaultIfEmpty(0)
            //        .Average());
            //    Console.WriteLine(_context.ConsumableHistories
            //        .Where(x => x.Consumable != null && !string.IsNullOrEmpty(x.Consumable.Model))
            //        .GroupBy(x => x.Consumable.Model)
            //        .Select(g => new { Model = g.Key, Count = g.Count() }) // Группируем
            //        .OrderByDescending(g => g.Count)
            //        .AsEnumerable() // Переключаем на клиентскую обработку
            //        .Select(g => g.Model)
            //        .FirstOrDefault() ?? "Не определено");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq111111111111111111111111" + ex.Message);

            //}

            var histories = _context.ConsumableHistories.ToList();

            var result = new StatisticDto
            {
                TechicalEquipment = new TechicalEquipmentStatisticDto
                {
                    TotalCount = technicalEquipments.Count,
                    FaultCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Faulty),
                    ActiveCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Serviceable),
                    WorkableCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Efficient),
                    InoperableCount = technicalEquipments.Count(x => x.State == ConditionEquipment.Inoperative),
                    WrittenOffCount = technicalEquipments.Count(x => x.IsDeleted)
                },
                Application = new ApplicationStatisticDto
                {
                    TotalCount = applications.Count,
                    SolvedCount = applications.Count(x => x.Status == ApplicationStatus.Solved),
                    NewCount = applications.Count(x => x.Status == ApplicationStatus.New),
                    InProgressRequestsCount = applications.Count(x => x.Status == ApplicationStatus.InProgress),
                    TransferredCount = applications.Count(x => x.Status == ApplicationStatus.Transferred),
                    SuspendedCount = applications.Count(x => x.Status == ApplicationStatus.Suspended)
                },
                Consumable = new ConsumableStatisticDto
                {
                    TotalCount = consumables.Count,
                    InStockCount = consumables.Count(x => x.Status == "В наличии"),
                    LowStockCount = consumables.Count(x => x.Status == "Малый запас"),
                    OutOfStockCount = consumables.Count(x => x.Status == "Отсутствует"),
                    AvgUsagePerMonth = histories.Where(x => x.DateOfOperation >= DateTime.Now.AddMonths(-6)) // За последние 6 мес
                                                                   .GroupBy(x => x.DateOfOperation.Value.Month)
                                                                   .Select(g => g.Sum(x => x.Quantity))
                                                                   .DefaultIfEmpty(0)
                                                                   .Average(), // Средний расход
                    MostUsedConsumable = histories.Where(x => x.Consumable != null && !string.IsNullOrEmpty(x.Consumable.Model))
                                                                    .GroupBy(x => x.Consumable.Model)
                                                                     .OrderByDescending(g => g.Count())
                                                                     .Select(g => g.Key)
                                                                     .FirstOrDefault() ?? "Не определено",
                },



                // 📌 Количество неисправностей по месяцам
                FaultsByMonth = applications
                    .Where(t => t.Status != ApplicationStatus.Solved) // ✅ Проверяем, что есть дата
                    .GroupBy(t => t.DateOfCreation.Month)
                    .Select(g => new FaultsByMonthDto
                    {
                        Month = new DateTime(2025, g.Key, 1).ToString("MMMM"),
                        FaultCount = g.Count()
                    })
                    .ToList(),

                // 📌 Топ-5 самых ненадёжных моделей
                //FaultyEquipment = _context.TechnicalEquipment
                //    .Where(t => t.Model != null && t.Brand != null) // ✅ Исключаем NULL
                //    .GroupBy(t => new
                //    {
                //        ModelName = t.Model.Name,
                //        BrandName = t.Brand.Name ?? "Неизвестный бренд"
                //    })
                //    .OrderByDescending(g => g.Count())
                //    .Take(5)
                //    .Select(g => new FaultyEquipmentDto
                //    {
                //        EquipmentModel = g.Key.ModelName,
                //        Brand = g.Key.BrandName,
                //        FaultCount = g.Count()
                //    })
                //    .ToList(),

                //// 📌 Топ-5 расходников
                //TopConsumables = _context.ConsumableHistories
                //    .Where(c => c.Consumable != null && !string.IsNullOrEmpty(c.Consumable.Model)) // ✅ Проверяем NULL
                //    .GroupBy(c => c.Consumable.Model)
                //    .OrderByDescending(g => g.Count())
                //    .Take(5)
                //    .Select(g => new TopConsumablesDto
                //    {
                //        ConsumableName = g.Key,
                //        UsageCount = g.Count()
                //    })
                //    .ToList(),

                //// 📌 Среднее время закрытия заявки по категориям
                //AvgClosureTime = _context.Applications
                //    .Where(a => a.DateOfClosing != null && a.Category != null) // ✅ Проверяем NULL
                //    .GroupBy(a => a.Category.Name)
                //    .Select(g => new AvgClosureTimeDto
                //    {
                //        Category = g.Key,
                //        AvgDays = g.Average(a => (a.DateOfClosing.Value - a.DateOfCreation).TotalDays)
                //    })
                //    .ToList(),

                //// 📌 Частые категории неисправностей
                //FaultCategories = _context.Applications
                //    .Where(a => a.Category != null) // ✅ Исключаем NULL
                //    .GroupBy(a => a.Category.Name)
                //    .OrderByDescending(g => g.Count())
                //    .Select(g => new FaultCategoryDto
                //    {
                //        CategoryName = g.Key,
                //        Count = g.Count()
                //    })
                //    .ToList()
            };

            return View(result);
        }

        //// Контроллер API для получения статистики по заявкам
        //[HttpGet]
        //public async Task<IActionResult> GetApplicationStatistics()
        //{
        //    var applications = await _context.Applications.ToListAsync();

        //    var statusCounts = applications
        //        .GroupBy(a => a.Status)
        //        .OrderByDescending(g => g.Count())
        //        .Take(5)
        //        .Select(g => new { Status = g.Key.ToString(), Count = g.Count() })
        //        .ToList();

        //    return Json(new
        //    {
        //        labels = statusCounts.Select(s => s.Status),
        //        counts = statusCounts.Select(s => s.Count)
        //    });
        //}
    }
}

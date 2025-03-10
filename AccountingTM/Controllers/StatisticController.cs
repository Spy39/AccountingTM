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

        public IActionResult Index()
        {
            var technicalEquipments = _context.TechnicalEquipment.ToList();
            var applications = _context.Applications.ToList();
            var consumables = _context.Consumables.ToList();
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

                //// 📌 Количество неисправностей по месяцам
                //FaultsByMonth = applications
                //    .Where(t => t.Status != ApplicationStatus.Solved) // ✅ Проверяем, что есть дата
                //    .GroupBy(t => t.DateOfCreation.Month)
                //    .Select(g => new FaultsByMonthDto
                //    {
                //        Month = new DateTime(2025, g.Key, 1).ToString("MMMM"),
                //        FaultCount = g.Count()
                //    })
                //    .ToList(),


                // 🔴 Количество неисправностей по месяцам
                FaultsByMonth = applications
                    .Where(t => t.Status != ApplicationStatus.Solved)
                    .GroupBy(t => t.DateOfCreation.Month)
                    .Select(g => new FaultsByMonthDto
                    {
                        Month = new DateTime(DateTime.Now.Year, g.Key, 1).ToString("MMMM"),
                        FaultCount = g.Count()
                    })
                    .ToList(),

                // 🔴 Топ-5 самых ненадёжных моделей
                FaultyEquipment = _context.TechnicalEquipment
                    .Where(t => t.Model != null && t.Brand != null)
                    .GroupBy(t => new { ModelName = t.Model.Name, BrandName = t.Brand.Name ?? "Неизвестный бренд" })
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new FaultyEquipmentDto
                    {
                        EquipmentModel = g.Key.ModelName,
                        Brand = g.Key.BrandName,
                        FaultCount = g.Count()
                    })
                    .ToList(),

                // 🔵 Топ-5 расходников
                TopConsumables = _context.ConsumableHistories
                    .Where(c => c.Consumable != null && !string.IsNullOrEmpty(c.Consumable.Model))
                    .GroupBy(c => c.Consumable.Model)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new TopConsumablesDto
                    {
                        ConsumableName = g.Key,
                        UsageCount = g.Count()
                    })
                    .ToList(),

                // 🟠 Среднее время закрытия заявки по категориям
                AvgClosureTime = _context.Applications
                    .Where(a => a.DateOfClosing != null && a.Category != null)
                    .GroupBy(a => a.Category.Name)
                    .Select(g => new AvgClosureTimeDto
                    {
                        Category = g.Key,
                        AvgDays = g.Average(a => (a.DateOfClosing.Value - a.DateOfCreation).TotalDays)
                    })
                    .ToList(),

                // 🟠 Частые категории неисправностей
                FaultCategories = _context.Applications
                    .Where(a => a.Category != null)
                    .GroupBy(a => a.Category.Name)
                    .OrderByDescending(g => g.Count())
                    .Select(g => new FaultCategoryDto
                    {
                        CategoryName = g.Key,
                        Count = g.Count()
                    })
                    .ToList()
            

        };

            return View(result);
        }

        // 📊 API: Получение статистики заявок
        [HttpGet]
        public async Task<IActionResult> GetApplicationStatistics()
        {
            var applications = await _context.Applications.ToListAsync();
            var statusCounts = applications
                .GroupBy(a => a.Status)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Status = g.Key.ToString(), Count = g.Count() })
                .ToList();

            return Json(new
            {
                labels = statusCounts.Select(s => s.Status),
                counts = statusCounts.Select(s => s.Count)
            });
        }

        // 📊 API: Получение неисправностей по месяцам
        [HttpGet]
        public async Task<IActionResult> GetFaultsByMonth()
        {
            var faults = await _context.Applications
                .Where(a => a.Status != ApplicationStatus.Solved)
                .GroupBy(a => a.DateOfCreation.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();

            return Json(new
            {
                labels = faults.Select(f => new DateTime(DateTime.Now.Year, f.Month, 1).ToString("MMMM")),
                counts = faults.Select(f => f.Count)
            });
        }

        // 📊 API: Получение топ-5 расходников
        [HttpGet]
        public async Task<IActionResult> GetTopConsumables()
        {
            var consumables = await _context.ConsumableHistories
                .Where(c => c.Consumable != null)
                .GroupBy(c => c.Consumable.Model)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .ToListAsync();

            return Json(new
            {
                labels = consumables.Select(c => c.Name),
                counts = consumables.Select(c => c.Count)
            });
        }

        // 📊 API: Получение средних времен закрытия заявок
        [HttpGet]
        public async Task<IActionResult> GetAvgClosureTime()
        {
            var avgTimes = await _context.Applications
                .Where(a => a.DateOfClosing != null && a.Category != null)
                .GroupBy(a => a.Category.Name)
                .Select(g => new { Category = g.Key, AvgDays = g.Average(a => (a.DateOfClosing.Value - a.DateOfCreation).TotalDays) })
                .ToListAsync();

            return Json(new
            {
                labels = avgTimes.Select(a => a.Category),
                counts = avgTimes.Select(a => a.AvgDays)
            });
        }
    }
}

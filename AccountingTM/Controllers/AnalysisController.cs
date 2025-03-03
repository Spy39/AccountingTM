using Accounting.Data;
using AccountingTM.Dto.Analysis;
using AccountingTM.Forecasting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
	[Authorize]
	public class AnalysisController : Controller
	{
		private readonly DataContext _context;

		public AnalysisController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public IActionResult GetConsumableForecast([FromBody] CalculateRequestDto input)
        {
            Console.WriteLine($"📡 Запрос прогноза расходных материалов: {input.Model}");

            if (string.IsNullOrEmpty(input.Model))
            {
                return Json(new { error = "⚠️ Не указана модель расходника!" });
            }

            var history = _context.ConsumableHistories
                .Where(x => x.Consumable.Model == input.Model)
                .OrderBy(x => x.DateOfOperation)
                .ToList();

            if (!history.Any())
            {
                return Json(new { error = "⚠️ Нет данных по указанному расходному материалу!" });
            }

            var totalUsage = history.Sum(x => x.Quantity);
            var monthsCount = history.Select(x => x.DateOfOperation.Value.Month).Distinct().Count();
            var avgUsagePerMonth = monthsCount > 0 ? (float)totalUsage / monthsCount : 0;

            var forecast = new List<object>();
            var today = DateTime.Now;

            for (int i = 1; i <= 5; i++)
            {
                var nextMonth = today.AddMonths(i);
                float predictedUsage = avgUsagePerMonth + new Random().Next(-5, 5);

                forecast.Add(new
                {
                    month = nextMonth.ToString("MMMM yyyy"),
                    usage = Math.Max(0, Math.Round(predictedUsage, 1))
                });
            }

            Console.WriteLine($"✅ Прогноз сгенерирован: {forecast.Count} месяцев");

            return Json(forecast);
        }
        //[HttpPost]
        //      public IActionResult Calculate([FromBody] CalculateRequestDto input)
        //      {
        //          var analysisAppService = new ForecastingAppService();

        //          // Найдём названия бренда и расходника в БД
        //          var brand = _context.Brands.Find(input.BrandId);
        //          var typeC = _context.TypeConsumables.Find(input.TypeConsumableId);

        //          // Заполним модель для вашего ForecastingAppService
        //          var forecastModel = new ConsumableAnalisisModel
        //          {
        //              Brand = brand?.Name,
        //              TypeConsumable = typeC?.Name,
        //              Model = input.Model,
        //              Mounth = input.DateStart.Month,
        //              Year = input.DateStart.Year
        //          };

        //          // Получим некий quantity
        //          var quantity = analysisAppService.AnalysisConsumable(forecastModel);

        //          // Сгенерируем массив из 5 «точек». Или 12, или сколько нужно.
        //          var random = new Random();
        //          var resultList = new List<object>();

        //          for (int i = 1; i <= 5; i++)
        //          {
        //              // Небольшое случайное отклонение
        //              float offset = (float)(random.NextDouble() * 10.0 - 5.0); // -5..+5
        //              float value = quantity + offset;
        //              if (value < 0) value = 0;

        //              // Пример: "Месяц i" + value
        //              resultList.Add(new
        //              {
        //                  month = $"Месяц {i}",
        //                  val = value
        //              });
        //          }

        //          // Вернём это в формате JSON
        //          return Ok(resultList);
        //      }


        //      [HttpPost]
        //public IActionResult Training([FromBody] CalculateRequestDto input)
        //{
        //	var consumables = _context.ConsumableHistories.ToList();


        //	return Ok();
        //}
    }
}

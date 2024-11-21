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
		public IActionResult Calculate([FromBody]CalculateRequestDto input)
		{
			var analysisAppService = new ForecastingAppService();
			var brandName = _context.Brands.Find(input.BrandId).Name;
			var typeConsumableName = _context.TypeConsumables.Find(input.TypeConsumableId).Name;

			var quantity = analysisAppService.AnalysisConsumable(new ConsumableAnalisisModel
			{
				Brand = brandName,
				TypeConsumable = typeConsumableName,
				Model = input.Model,
				Mounth = input.DateStart.Month,
				Year = input.DateStart.Year,
			});
			return Ok();
		}

		[HttpPost]
		public IActionResult Training([FromBody] CalculateRequestDto input)
		{
			var consumables = _context.ConsumableHistories.ToList();
			

			return Ok();
		}
	}
}

using Accounting.Data;
using AccountingTM.Dto.Analysis;
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

			return View();
		}
	}
}

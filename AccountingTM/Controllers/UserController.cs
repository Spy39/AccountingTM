using Accounting.Data;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreatePage()
        {
            return new PartialViewResult
            {
                ViewName = "CreateUser"
            };
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Ok");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Ok");
        }

        [HttpGet]
        public IActionResult Find()
        {
            return Ok("Ok");
        }

        [HttpPatch]
        public IActionResult Update()
        {
            return Ok("Ok");
        }
    }
}
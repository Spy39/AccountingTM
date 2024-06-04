﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

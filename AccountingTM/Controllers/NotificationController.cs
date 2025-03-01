using Accounting.Data;
using AccountingTM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly DataContext _context;

        public NotificationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllUserNotifications()
        {
            var userId = int.Parse(User.Identity.Name); // Получаем ID текущего пользователя
            var notifications = _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return Json(notifications);
        }

        [HttpPost]
        public IActionResult MarkAsRead()
        {
            var userId = int.Parse(User.Identity.Name);
            var notifications = _context.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToList();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return Ok();
        }
    }
}

using Accounting.Data;
using AccountingTM.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var technicalEquipments = _context.TechnicalEquipment.Where(x => x.DateGarant.HasValue && x.DateGarant <= DateTime.Now).Include(x => x.Brand).Include(x => x.Model).ToList();
            var notifications = technicalEquipments.Select(x => new Notification
            {
                CreatedAt = DateTime.Now,
                IsRead = false,
                 Message = $"Истек срок гарантии {x.Brand.Name} {x.Model.Name} {x.SerialNumber}",
            });

            return Ok(notifications);
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

using System;

namespace AccountingTM.Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Для кого уведомление
        public string Message { get; set; }
        public bool IsRead { get; set; } = false; // Прочитано или нет
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models.Directory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Учет заявок
    /// </summary>
    public class Application : Entity
	{
		public int LocationId { get; set; }
		[ForeignKey(nameof(LocationId))]
		public Location Location { get; set; } //Местоположение
		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } // Категория
		/// <summary>Номер заявки</summary>
		public string ApplicationNumber { get; set; }
		/// <summary>Дата создания</summary>
		public DateTime DateOfCreation { get; set; }
		/// <summary>Дата изменения</summary>
		public DateTime DateOfChange { get; set; }
		/// <summary>Срок истечения</summary>
		public DateTime? ExpirationDate { get; set; }
		/// <summary>Тема</summary>
		public string Subject { get; set; }
		///<summary>Описание</summary>
		public string Description { get; set; }
		/// <summary>Статус</summary>
		public ApplicationStatus Status { get; set; }
		/// <summary>Автор</summary>
		public string Author { get; set; }
		/// <summary>Исполнитель</summary>
		public string? Executor { get; set; }
		/// <summary>Последний ответивший</summary>
		public string? LastReply { get; set; }
		public Priority Priority { get; set; }

	}
}

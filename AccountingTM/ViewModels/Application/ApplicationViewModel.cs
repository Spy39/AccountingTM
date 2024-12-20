﻿using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.ViewModels.Application
{
	public class ApplicationViewModel
	{
		public int ApplicationId { get; set; }
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
		public string Status { get; set; }
		public string Location { get; set; } //Местоположение
		public string Category { get; set; } // Категория
		/// <summary>Автор</summary>
		public string Author { get; set; }
		/// <summary>Исполнитель</summary>
		public string? Executor { get; set; }
		/// <summary>Последний ответивший</summary>
		public string? LastReply { get; set; }
		public string Priority { get; set; }
	}
}

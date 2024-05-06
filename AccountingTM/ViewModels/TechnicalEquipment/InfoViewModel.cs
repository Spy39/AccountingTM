﻿using Accounting.Models;
using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.ViewModels.TechnicalEquipment
{
	public class InfoViewModel
	{
		public int TechnicalId { get; set; }
		public string Model { get; set; }
		public string? SerialNumber { get; set; }
		public string InventoryNumber { get; set; }
		public string EmployeeFio {  get; set; }
		public string LocationName { get; set; }
		/// <summary>
		/// Дата изготовления
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Дата ввода в эксплуатацию
		/// </summary>
		public DateTime DateStart { get; set; }
		/// <summary>
		/// Средний срок работы
		/// </summary>
		public DateTime? DateEnd { get; set; }
		/// <summary>
		/// Дата действия гарантии
		/// </summary>
		public DateTime? DateGarant { get; set; }
		public TechnicalStatus Status { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
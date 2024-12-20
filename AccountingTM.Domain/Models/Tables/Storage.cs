﻿using System;
using System.Collections.Generic;
using Accounting.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingTM.Models;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Хранение
    /// </summary>
    public class Storage : Entity
	{
		public int TechnicalEquipmentId { get; set; }
		[ForeignKey(nameof(TechnicalEquipmentId))]
		public TechnicalEquipment? TechnicalEquipment { get; set; }
		/// <summary>
		/// Дата приемки на хранение
		/// </summary>
		public DateTime Acceptance {  get; set; }
		/// <summary>
		/// Дата снятия с хранения
		/// </summary>
		public DateTime Removal { get; set; }
		/// <summary>
		/// Условия хранения
		/// </summary>
		public string StorageConditions { get; set; }
		/// <summary>
		/// Вид хранения
		/// </summary>
		public string TypeOfStorage { get; set; }
	}
}

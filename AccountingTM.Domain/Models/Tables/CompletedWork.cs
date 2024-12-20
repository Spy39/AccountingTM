﻿using Accounting.Models;
using System;
using System.Collections.Generic;
using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountingTM.Models;

namespace AccountingTM.Domain.Models.Tables
{
    /// <summary>
    /// Учет выполненных работ
    /// </summary>
    public class CompletedWork : Entity
	{
		public int TechnicalEquipmentId { get; set; }
		[ForeignKey(nameof(TechnicalEquipmentId))]
		public TechnicalEquipment? TechnicalEquipment { get; set; }
		public int ApplicationsId { get; set; }
		[ForeignKey(nameof(ApplicationsId))]
		public Application Applications { get; set; }
		public DateTime Date { get; set; }
		/// <summary>
		/// Наименование работ и их причина
		/// </summary>
		public string NameAndReason { get; set; }
		/// <summary>
		/// Фамилия выполнившего работу
		/// </summary>
		public string Completed {  get; set; }
	}
}

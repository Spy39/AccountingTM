using AccountingTM.Domain.Models.Directory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models
{
    public class ApplicationHistory : Entity
    {
        public int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public Application? Application { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; } // Исполнитель
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;
        public string TypeOfOperation { get; set; }
        public string Name { get; set; }
    }
}

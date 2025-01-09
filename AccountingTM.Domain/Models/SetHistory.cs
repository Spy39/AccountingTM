using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Обозначение комплекта
    /// </summary>
    public class SetHistory : Entity
    {
        public Set Set { get; set; }
        public int SetId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; } //Ответственный			
        /// <summary>Дата операции</summary>///
        public DateTime? DateOfOperation { get; set; }
        /// <summary>Тип операции</summary>///
        public string? TypeOfOperation { get; set; }
    }
}
using AccountingTM.Domain.Models.Directory;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Обозначение комплекта
    /// </summary>
    public class Set : Entity
    {

        public int? LocationId { get; set; }
        public Location? Location { get; set; } //Местоположение
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; } //Ответственный			
        /// <summary>Наименование комплекта</summary>///
        public string Name { get; set; }
        /// <summary>Статус комплекта</summary>///
        public bool IsStatusSet { get; set; }
        public string StatusSet => IsStatusSet ? "Полный" : "Неполный";
    }
}
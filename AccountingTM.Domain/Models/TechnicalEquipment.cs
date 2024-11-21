using AccountingTM.Domain;
using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Models
{
    /// <summary>
    /// Технические средства
    /// </summary>
    public class TechnicalEquipment : Entity
    {       
        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public TypeEquipment? Type { get; set; } //Тип технического средства												 
		public int BrandId { get; set; }
		[ForeignKey(nameof(BrandId))]
		public Brand? Brand { get; set; } //Бренд										 
		public int EmployeeId { get; set; }
		[ForeignKey(nameof(EmployeeId))]
		public Employee? Employee { get; set; } //Ответственный											   
		public int LocationId { get; set; }
		[ForeignKey(nameof(LocationId))]
		public Location? Location { get; set; } //Местоположение технического средства
		public int? SetId { get; set; }
		public Set Set { get; set; }
		/// <summary>
		/// Модель технического средства
		/// </summary>
		public string Model { get; set; }
		public string? SerialNumber { get; set; }
		public string? InventoryNumber { get; set; }
		/// <summary>
		/// Дата изготовления
		/// </summary>
		public DateTime? Date {  get; set; }
		/// <summary>
		/// Дата ввода в эксплуатацию
		/// </summary>
		public DateTime? DateStart { get; set; }
		/// <summary>
		/// Средний срок работы
		/// </summary>
		public DateTime? DateEnd { get; set; }
		/// <summary>
		/// Средний срок работы в месяцах
		/// </summary>
		public int? WorkTimeAvg { get; set; }
		/// <summary>
		/// Дата действия гарантии
		/// </summary>
		public DateTime? DateGarant { get; set; }
		public ConditionEquipment State { get; set; } //Состояние
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedDate { get; set; }

		public string GetStatus()
		{
			switch (State)
			{
				case ConditionEquipment.Serviceable:
					return "Исправно";
                case ConditionEquipment.Faulty:
                    return "Неисправно";
                case ConditionEquipment.Efficient:
                    return "Работоспособно";
                case ConditionEquipment.Inoperative:
                    return "Неработоспособно";
				default: return "";

            }
		}
    }




}

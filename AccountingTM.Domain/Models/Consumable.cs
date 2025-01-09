using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models
{
    /// <summary>
    /// Расходные материалы
    /// </summary>
    public class Consumable : Entity
    {
        public int TypeConsumableId { get; set; }
        [ForeignKey(nameof(TypeConsumableId))]
        public TypeConsumable TypeConsumable { get; set; } //Тип расходного матерала
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; } //Бренд		
        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } //Местоположение
        public int UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; } // Единица измерения
        /// <summary>
        /// Модель расходного материала
        /// </summary>
        public string Model { get; set; }
        /// <summary>Количество</summary>
        public double Quantity { get; set; }
        /// <summary>Дата последнего пополнения</summary>///
        public DateTime? DateLatestAddition { get; set; }
        public double SmallStockValue { get; set; }

        /// <summary>Статус</summary>/// 
        public string Status
        {
            get
            {
                if (Quantity == 0)
                {
                    return "Отсутствует";
                }
                else if (Quantity <= SmallStockValue)
                {
                    return "Малый запас";
                }
                return "В наличии";
            }
        }
    }
}

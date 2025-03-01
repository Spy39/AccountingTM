namespace AccountingTM.Dto.Statistics
{
    public class TechicalEquipmentStatisticDto
    {
        public int TotalCount { get; set; } //всего
        public int ActiveCount { get; set; } //исправные
        public int FaultCount { get; set; } //неисправные
        public int WorkableCount { get; set; } //работоспособные
        public int InoperableCount { get; set; } //неработоспособные
        public int WrittenOffCount { get; set; }//списанные
    }
}

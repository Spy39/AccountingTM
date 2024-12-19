namespace AccountingTM.Dto.Set
{
    public class CreateCompoundSetDto
    {
        public int SetId { get; set; }
        public IEnumerable<int> TechnicalEquipmentIds { get; set; }
    }
}

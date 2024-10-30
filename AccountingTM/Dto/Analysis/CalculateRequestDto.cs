namespace AccountingTM.Dto.Analysis
{
	public class CalculateRequestDto
	{
		public CategoryAnalysisType Category {  get; set; }
		public int TypeConsumableId { get; set; }
		public int BrandId { get; set; }
		public string Model { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
	}
}

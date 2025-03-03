namespace AccountingTM.Dto.Common
{
    public class SearchPagedRequestDto : PagedRequestDto
    {
        public string? SearchQuery { get; set; }

        // свойства для фильтрации по диапазону дат
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public bool? InStock { get; set; }
    }
}

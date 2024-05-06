namespace AccountingTM.Dto.Common
{
    public class SearchPagedRequestDto : PagedRequestDto
    {
        public string? SearchQuery { get; set; }
    }
}

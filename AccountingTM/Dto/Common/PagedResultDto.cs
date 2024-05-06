namespace AccountingTM.Dto.Common
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}

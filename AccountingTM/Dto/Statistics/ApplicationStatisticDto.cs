namespace AccountingTM.Dto.Statistics
{
    public class ApplicationStatisticDto
    {
        public int TotalCount { get; set; } //всего
        public int SolvedCount { get; set; } //решены
        public int NewCount { get; set; } //новые
        public int InProgressRequestsCount { get; set; } //в работе
        public int TransferredCount { get; set; } //переданы
        public int SuspendedCount { get; set; }//приостановлены
    }
}

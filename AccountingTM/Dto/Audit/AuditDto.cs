namespace AccountingTM.Dto.Audit
{
    public class AuditDto
    {
        public DateTime Date { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
        public int PrimaryKey { get; set; }

    }
}

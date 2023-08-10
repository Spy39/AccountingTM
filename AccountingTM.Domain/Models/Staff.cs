using AccountingTM.Domain.Models;

namespace Accounting.Models
{
    public class Staff : Entity
    {
        public string Surname { get; set; }
        public string NameStaff { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
    }
}

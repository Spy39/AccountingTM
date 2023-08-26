using AccountingTM.Domain.Models;

namespace Accounting.Models
{
    public class Staff : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
    }
}

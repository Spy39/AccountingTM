using AccountingTM.Domain;

namespace AccountingTM.Domain.Models.Directory
{
    /// <summary>
    /// Ответственный
    /// </summary>
    public class Employee : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        /// <summary>Должность</summary>
        public string? Position { get; set; }
    }
}

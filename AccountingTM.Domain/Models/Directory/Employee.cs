using AccountingTM.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingTM.Domain.Models.Directory
{
    /// <summary>
    /// Ответственный (сотрудник)
    /// </summary>
    public class Employee : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        /// <summary>Должность</summary>
        public string? Position { get; set; }
        [NotMapped]
        public string FullName => $"{LastName} {FirstName} {FatherName}";
    }
}

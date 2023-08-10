using AccountingTM.Domain.Enums;
using AccountingTM.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class TechnicalEquipment : Entity
    {
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public TechnicalStatus Status { get; set; }
        public string? Responsible { get; set; }
        public string? Location { get; set; }   
        public DateTime? DeletedDate { get; set; }

    }
}

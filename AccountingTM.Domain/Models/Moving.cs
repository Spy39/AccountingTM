using AccountingTM.Domain;
using AccountingTM.Domain.Models;
using AccountingTM.Domain.Models.Directory;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Models
{
    public class Moving : Entity
    {
        public int DocumentTypeId { get; set; }
        
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }

        public int DocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public int ConveyedId { get; set; }

        [ForeignKey("ConveyedId")]
        public Employee Conveyed { get; set; }
        
        public int NewRespId { get; set; }

        [ForeignKey("NewRespId")]
        public Employee NewResp { get; set; }

        public int TechnicalEquipmentId { get; set; }
        
        [ForeignKey("TechnicalEquipmentId")]
        public TechnicalEquipment TechnicalEquipment { get; set; }
    }
}

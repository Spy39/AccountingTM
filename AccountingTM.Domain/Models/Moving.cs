using AccountingTM.Domain.Models;
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
        public Staff Conveyed { get; set; }
        
        public int NewRespId { get; set; }

        [ForeignKey("NewRespId")]
        public Staff NewResp { get; set; }

        public int TechnicalEquipmentId { get; set; }
        
        [ForeignKey("TechnicalEquipmentId")]
        public TechnicalEquipment TechnicalEquipment { get; set; }
    }
}

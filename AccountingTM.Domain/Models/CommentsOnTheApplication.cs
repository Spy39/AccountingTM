using AccountingTM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models
{
    public class CommentsOnTheApplication : Entity
    {
        public int ApplicationId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public Application? Application { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;
        public string Text { get; set; }
        public string PathToFile { get; set; }
    }
}

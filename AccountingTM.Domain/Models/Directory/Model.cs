using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Directory
{
    public class Model : Entity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

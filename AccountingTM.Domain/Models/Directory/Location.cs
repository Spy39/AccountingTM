using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models.Directory
{
    /// <summary>
    /// Местоположение (помещение)
    /// </summary>
    public class Location : Entity
    {
        public string Name { get; set; }
    }
}

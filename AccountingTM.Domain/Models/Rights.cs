﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTM.Domain.Models
{
    public class Rights : Entity
    {
        public string Name { get; set; }
        public string Connection { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class AgroToxic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ToxicLevel ToxicLevel { get; set; }
        public string Description { get; set; }
    }
}

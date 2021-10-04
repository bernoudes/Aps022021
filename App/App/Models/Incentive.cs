using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class Incentive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeIncentive TypeOfIncentive  { get; set; }
        public string Description { get; set; }
    }
}

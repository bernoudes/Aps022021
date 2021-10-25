using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class Incentive
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeIncentive TypeOfIncentive  { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Company_has_AgroToxic")]
    public class CompanyAgrotoxic
    {
        public int Company_id { get; set; }
        public int AgroToxic_id { get; set; }
        public Company Company { get; set; }
        public AgroToxic AgroToxic { get; set; }
    }
}

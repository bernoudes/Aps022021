using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Company_has_Tax")]
    public class CompanyTax
    {
        public int Company_id { get; set; }
        public int Tax_id { get; set; }
        public Company Company { get; set; }
        public Tax Tax { get; set; }
    }
}

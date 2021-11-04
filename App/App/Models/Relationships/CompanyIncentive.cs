using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Company_has_Incentive")]
    public class CompanyIncentive
    {
        [ForeignKey("Company_id")]
        public int Company_id { get; set; }
        [ForeignKey("Incentive_id")]
        public int Incentive_id { get; set; }
        public Company Company { get; set; }
        public Incentive Incentive { get; set; }
    }
}

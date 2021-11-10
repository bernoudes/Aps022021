using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address_Street { get; set; }
        public string Address_Number { get; set; }
        public string Address_CEP { get; set; }
        public int NumberOfEmployees { get; set; }
        public int QuantityOfMachines { get; set; }
        public double AnualProductionKilos { get; set; }

        public AutomationLevel AutomationLevel { get; set; }
        public TargetMarket TargetMarket { get; set; }
        public virtual ICollection<CompanyProduct> CompanyProduct { get; set; }
        public virtual ICollection<CompanyAgrotoxic> CompanyAgrotoxic { get; set; }
        public virtual ICollection<CompanyIncentive> CompanyIncentive { get; set; }
        public virtual ICollection<CompanyTax> CompanyTax { get; set; }
    }
}

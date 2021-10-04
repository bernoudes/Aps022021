using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfEmployees { get; set; }
        public int QuantityOfMachines { get; set; }
        public double AnualProductionKilos { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<AgroToxic> AgroToxics { get; set; } = new List<AgroToxic>();
        public ICollection<Incentive> Incentives { get; set; } = new List<Incentive>();
        public ICollection<Tax> Taxes { get; set; } = new List<Tax>();
    }
}

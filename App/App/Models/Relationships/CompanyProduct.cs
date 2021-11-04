using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Company_has_Product")]
    public class CompanyProduct
    {   
        [ForeignKey("Product_id")]
        public int Product_id { get; set; }
        [ForeignKey("Company_id")]
        public int Company_id { get; set; }

        public Product Product;
        public Company Company;
    }
}

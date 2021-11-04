using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Models
{
    
    public class AgroToxic
    {

        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public ToxicLevel ToxicLevel { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CompanyAgrotoxic> CompanyAgrotoxic { get; set; }
    }
}

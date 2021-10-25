using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Tax
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Description { get; set; }
    }
}

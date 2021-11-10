using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Enums
{
    public enum ToxicLevel : int
    {
        [Display(Name = "Baixo")]
        Low = 1,
        [Display(Name = "Medio")]
        Medium = 2,
        [Display(Name = "Alto")]
        High = 3
    }
}

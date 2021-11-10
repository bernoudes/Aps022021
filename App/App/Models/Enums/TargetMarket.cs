using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Enums
{
    public enum TargetMarket : int
    {
        [Display(Name ="Interno")]
        Internal = 1,
        [Display(Name = "Externo")]
        External = 2
    }
}

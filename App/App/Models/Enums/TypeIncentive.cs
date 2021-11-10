using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Enums
{
    public enum TypeIncentive : int
    {
        [Display(Name ="Crédito")]
        Credit = 1,
        [Display(Name = "Isenção")]
        Exempt = 2
    }
}

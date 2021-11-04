using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Enums
{
    public enum AutomationLevel : int
    {
        [Display(Name="Menos Que 20%")]
        Less20 = 1,
        [Display(Name = "Entre 20% e 50%")]
        Between20and50 = 2,
        [Display(Name = "Entre 50% e 80%")]
        Between50and80 = 3,
        [Display(Name = "Entre 80% e 90%")]
        Between80and90 = 4
    }
}

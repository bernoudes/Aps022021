using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Enums
{
    [Flags]
    public enum UserLevel : int
    {
        [Display(Name = "Acesso Publico")]
        Public = 1,
        [Display(Name = "Acesso Nivel 2")]
        PeopleTwo = 2,
        [Display(Name = "Acesso Nivel 3")]
        Minister = 3,
    }
}

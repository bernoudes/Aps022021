using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class User
    {
        [Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}

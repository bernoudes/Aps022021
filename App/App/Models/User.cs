using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace App.Models
{
    public class User
    {
        [Key()]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Position { get; set; }
        public UserLevel UserLevel { get; set; }
        public Byte[] Image { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}

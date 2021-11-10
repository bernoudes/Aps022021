using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.ViewModel
{
    public class UserLoginViewModel
    {
        public string Email { get; set; }
        public IFormFile ImgFile { get; set; }
    }
}

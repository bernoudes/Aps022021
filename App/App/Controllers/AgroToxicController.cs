﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class AgroToxicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

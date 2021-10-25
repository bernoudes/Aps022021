using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Models;

namespace App.Controllers
{
    public class AgroToxicController : Controller
    {
        private readonly AgroToxicService _agroToxicService;

        public AgroToxicController(AgroToxicService agroToxicService)
        {
            _agroToxicService = agroToxicService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _agroToxicService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgroToxic agroToxic)
        {
        }*/
    }
}

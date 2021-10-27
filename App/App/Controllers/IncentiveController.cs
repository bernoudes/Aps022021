using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Models;

namespace App.Controllers
{
    public class IncentiveController : Controller
    {
        private readonly IncentiveService _incentiveService;

        public IncentiveController(IncentiveService incentiveService)
        {
            _incentiveService = incentiveService;
        }

        //LIST
        public async Task<IActionResult> Index()
        {
            var list = await _incentiveService.FindAllAsync();
            return View(list);
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Incentive obj)
        {
            await _incentiveService.InsertAsync(obj);
            return RedirectToAction(nameof(Index));
        }

        //UPDATE
        public async Task<IActionResult>
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Models;
using App.Services.Exceptions;
using System.Diagnostics;

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
        public async Task<IActionResult>Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var obj = await _incentiveService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Incentive incentive)
        {
            if (!ModelState.IsValid)
            {
                return View(incentive);
            }
            if (id != incentive.Id)
            {
                return BadRequest();
            }
            try
            {
                await _incentiveService.UpdateAsync(incentive);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Index), new { message = e.Message });
            }
        }

            //REMOÇÃO
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Index), new { message = "id not provide" });
            }
            var obj = await _incentiveService.FindById(id.Value);
            if(obj == null)
            {
            return RedirectToAction(nameof(Index), new { message = "id not found" });
            }
             return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _incentiveService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                TempData["message"] = "Não Foi Possível Excluir as informações do Incentive no banco de dados" +
                    " outros dados dependem dele para existir.";
                return RedirectToAction(nameof(Index));
            }
        }

        //ERRO
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}

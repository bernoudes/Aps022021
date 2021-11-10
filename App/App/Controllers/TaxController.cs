using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Services.Exceptions;
using App.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize(Roles = "Minister")]
    public class TaxController : Controller
    {
        private readonly TaxService _taxService;

        public TaxController(TaxService taxService)
        {
            _taxService = taxService;
        }

        //LIST
        public async Task<IActionResult> Index()
        {
            var list = await _taxService.FindAllAsync();
            return View(list);
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Tax tax)
        {
            await _taxService.InsertAsync(tax);
            return RedirectToAction(nameof(Index));
        }

        //UPDATE
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var obj = await _taxService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tax tax)
        {
            if (!ModelState.IsValid)
            {
                return View(tax);
            }
            if (id != tax.Id)
            {
                return BadRequest();
            }
            try
            {
                await _taxService.UpdateAsync(tax);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index), new { message = "id not provide" });
            }
            var obj = await _taxService.FindByIdAsync(id.Value);
            if (obj == null)
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
                await _taxService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                TempData["message"] = "Não Foi Possível Excluir as informações do Taxas no banco de dados" +
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

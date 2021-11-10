using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Models;
using App.Services.Exceptions;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize(Roles = "Minister")]
    public class AgroToxicController : Controller
    {
        private readonly AgroToxicService _agroToxicService;

        public AgroToxicController(AgroToxicService agroToxicService)
        {
            _agroToxicService = agroToxicService;
        }

        //LIST
        public async Task<IActionResult> Index()
        {
            var list = await _agroToxicService.FindAllAsync();
            return View(list);
        }

        //CRIAÇÃO
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgroToxic agroToxic)
        {
            await _agroToxicService.InsertAsync(agroToxic);
            return RedirectToAction(nameof(Index));
        }


        //EDIÇÃO
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var obj = await _agroToxicService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgroToxic agroToxic)
        {
            if (!ModelState.IsValid)
            {
                return View(agroToxic);
            }
            if(id != agroToxic.Id)
            {
                return BadRequest();
            }
            try
            {
                await _agroToxicService.UpdateAsync(agroToxic);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //REMOÇÃO
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Index), new { message = "id not provide"});
            }
            var obj =  await _agroToxicService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Index), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _agroToxicService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                TempData["message"] = "Não Foi Possível Excluir as informações do Agrotóxico no banco de dados" +
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

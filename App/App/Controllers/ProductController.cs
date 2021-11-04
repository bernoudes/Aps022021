using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Services.Exceptions;
using App.Models;
using System.Diagnostics;

namespace App.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        //LIST
        public async Task<IActionResult> Index()
        {
            var list = await _productService.FindAllAsync();
            return View(list);
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            await _productService.InsertAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //UPDATE
        public async Task<IActionResult> Edit (int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var obj = await _productService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if(id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                await _productService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
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
            var obj = await _productService.FindByIdAsync(id.Value);
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
                await _productService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                TempData["message"] = "Não Foi Possível Excluir as informações do Produto no banco de dados" +
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

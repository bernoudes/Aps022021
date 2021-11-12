using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Services.Exceptions;
using App.Models;
using App.Models.ViewModel;
using App.Models.Enums;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;
        private readonly AgroToxicService _agroToxicService;
        private readonly IncentiveService _incentiveService;
        private readonly ProductService _productService;
        private readonly TaxService _taxService;

        public CompanyController( 
            CompanyService companyService,
            AgroToxicService agroToxicService,
            IncentiveService incentiveService,
            ProductService productService,
            TaxService taxService
            )
        {
            _companyService = companyService;
            _agroToxicService = agroToxicService;
            _incentiveService = incentiveService;
            _productService = productService;
            _taxService = taxService;
        }

        [Authorize(Roles = "Minister")]
        public async Task<IActionResult> Index()
        {
            var list = await _companyService.FindAllAsync();
            return View(list);
        }

        [Authorize(Roles = "Public")]
        public async Task<IActionResult> ListCompany()
        {
            var list = await _companyService.FindAllAsync();
            return View(list);
        }

        [Authorize(Roles = "Minister")]
        [Authorize(Roles = "PeopleTwo")]
        //CREATE
        public async Task<IActionResult> Create()
        {
            var agrotoxic = await _agroToxicService.FindAllAsync();
            var incentive = await _incentiveService.FindAllAsync();
            var tax = await _taxService.FindAllAsync();
            var product = await _productService.FindAllAsync();
            var company = new Company() {
                TargetMarket = TargetMarket.Internal,
                AutomationLevel = AutomationLevel.Less20
            };

            CompanyViewModel companyedit = new CompanyViewModel()
            {
                AgroToxic = agrotoxic,
                Incentive = incentive,
                Tax = tax,
                Product = product,
                Company = company
            };

            companyedit.SetValuesFalseCheck();
            return View(companyedit);
        }

        [Authorize(Roles = "Minister")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel companyViewModel)
        {
            await _companyService.InsetAsync(companyViewModel.Company);
            return RedirectToAction(nameof(Index));
        }

        //EDIT
        [Authorize(Roles = "Minister")]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var company = await _companyService.FindByIdAsync(id.Value);
            if(company == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var agrotoxic = await _agroToxicService.FindAllAsync();
            var incentive = await _incentiveService.FindAllAsync();
            var tax = await _taxService.FindAllAsync();
            var product = await _productService.FindAllAsync();
            
            CompanyViewModel companyedit = new CompanyViewModel() { 
                AgroToxic = agrotoxic,
                Incentive = incentive,
                Tax = tax,
                Product = product,
                Company = company
            };

            companyedit.SetValuesInCheck();

            return View(companyedit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Minister")]
        public async Task<IActionResult> Edit(int id, CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(companyViewModel);
            }
            if(id != companyViewModel.Company.Id)
            {
                return RedirectToAction(nameof(Index));
            }
             try
             {
                 companyViewModel.SetValuesOfCheckinCompany();
                 await _companyService.UpdateAsync(companyViewModel.Company);
                 return RedirectToAction(nameof(Index));
             }
             catch (NotFoundException e)
             {
                 return RedirectToAction(nameof(Error), new { message = "caraca" });
             }
        }

        //REMOVE
        [Authorize(Roles = "Minister")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index), new { message = "id not provide" });
            }
            var obj = await _companyService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Index), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Minister")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                TempData["message"] = "Não Foi Possível Excluir as informações do Agrotóxico no banco de dados" +
                    " outros dados dependem dele para existir.";

                return RedirectToAction(nameof(Index));
            }
        }
        //ERRO
        [Authorize(Roles = "Minister")]
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

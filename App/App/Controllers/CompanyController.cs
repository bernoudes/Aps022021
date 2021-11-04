using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using App.Services.Exceptions;
using App.Models;
using App.Models.ViewModel;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            var list = await _companyService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> ListCompany()
        {
            var list = await _companyService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(ListCompany));
            }

            var company = await _companyService.FindByIdAsync(id.Value);
            if(company == null)
            {
                return RedirectToAction(nameof(ListCompany));
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
                 return RedirectToAction(nameof(ListCompany));
             }
             catch (NotFoundException e)
             {
                 return RedirectToAction(nameof(Error), new { message = "caraca" });
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

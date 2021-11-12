using App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Services;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using App.Models.ViewModel;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View("login");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Validate(UserLoginViewModel userViewModel)
        {
            var userorigem = await _userService.FindByEmailAsync(userViewModel.Email);
            if (userorigem!= null && userorigem.Email != null) { 
                var value = ImgMethodsService.CompareIFormFileImgWithByteArray(userViewModel.ImgFile, userorigem.Image);

                if (value > 80) 
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, userorigem.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userorigem.Name));
                    claims.Add(new Claim(ClaimTypes.Email, userorigem.Email));
                    claims.Add(new Claim(ClaimTypes.Role, userorigem.UserLevel.ToString()));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (userorigem.UserLevel == Models.Enums.UserLevel.Public)
                    {
                        return RedirectToAction("ListCompany", "Company");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Company");
                    }
                }
            }
            else
            {
                TempData["Error"] = "Este email não pertence a nenhum usuário ativo";
            }
            TempData["Error"] = "A digital não bate com a do usuário";
            return View("login");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if(user.Name == null || user.Email == null)
            {
                TempData["Error"] = "Nome e Email não podem estar vazios";
                return View("Register");
            }

            else if (user.Name.Length < 4 || user.Email.Length < 4)
            {
                TempData["Error"] = "Nome e Email não podem ter menos que 4 caracteres";
                return View("Register");
            }

            else if (user.ImgFile != null && user.ImgFile.Length > 0)
            {
                var cont = user.ImgFile.ContentType;
                if(cont == "image/png" || cont == "image/bmp" || cont == "image/jpeg")
                {
                    user.Image = ImgMethodsService.ImageIFormForBytetArray(user.ImgFile);
                    user.UserLevel = Models.Enums.UserLevel.Public;
                    await _userService.InsertAsync(user);
                    return View("login");
                }
                else
                {
                    TempData["Error"] = "Nenhum arquivo do tipo bmp, jpg, png foi selecionado";
                    return View("Register");
                }
            }
            else
            {
                TempData["Error"] = "Nenhum arquivo do tipo bmp, jpg, png foi selecionado";
                return View("Register");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

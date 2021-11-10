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
                TempData["DataHere"] = $"Email: {userorigem.Email} \n percent: {value}";
            }
            else
            {
                TempData["DataHere"] = "useorigim is null";
            }

            
            return View();


           // var user = _userService.FindAndCompareFingerPrinterAsync(username, userfingerdata);
            
            /*
            if (username == "bob" && password == "pizza")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("usename", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, "Bob Edwar Jones"));
                claims.Add(new Claim(ClaimTypes.Role, "Minister"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if(returnUrl == null)
                {
                    return RedirectToAction("ListCompany", "Company");
                }
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error. Username or Password is invalid";*/
            return View("login");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if(user.Name == null || user.Email == null)
            {
                TempData["Error"] = "Nome e Email não podem estar vazios";
                return View();
            }

            else if (user.Name.Length < 4 || user.Email.Length < 4)
            {
                TempData["Error"] = "Nome e Email não podem ter menos que 4 caracteres";
                return View();
            }

            else if (user.ImgFile != null && user.ImgFile.Length > 0)
            {
                var cont = user.ImgFile.ContentType;
                if(cont == "image/png" || cont == "image/bmp" || cont == "image/jpg")
                {
                    user.Image = ImgMethodsService.ImageIFormForBytetArray(user.ImgFile);
                    user.UserLevel = Models.Enums.UserLevel.Public;
                    await _userService.InsertAsync(user);
                    return View("login");
                }
                else
                {
                    TempData["Error"] = "Nenhum arquivo do tipo bmp, jpg, png foi selecionado";
                    return View();
                }
            }
            return View("login");
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

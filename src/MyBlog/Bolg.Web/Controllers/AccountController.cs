using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bolg.Web.Models.ViewModels;
using Bolg.Web.UserContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bolg.Web.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly RoleManager<ApplicationUserRole> roleManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<ApplicationUserRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            //roleManager.CreateAsync(new IdentityRole("admin"));       
            //var role = roleManager.FindByNameAsync("admin");
            //roleManager.AddClaimAsync(role.Result, new Claim("admin","true"));
        }
        private IActionResult RedirectToUrl(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public IActionResult SignIn(string returnUrl = null)
        {
            //采用内建的模版，只有在你尚未经过验证或授权的情况下去尝试访问需授权的资源时，
            //returnUrl 才会被自动填入。当你尝试一个未授权的访问，安全中间件会根据 returnUrl 的设置将你重定向到登录页面。
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userModel = await userManager.FindByEmailAsync(signInViewModel.UserName);
            if (userModel != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(userModel, signInViewModel.PassWord, true, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToUrl(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "账号密码错误");
                }
            }
            else
            {
                ModelState.AddModelError("", "账号密码错误");
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var userModel = new ApplicationUser
                {
                    UserName = signUpViewModel.Email,
                    Email = signUpViewModel.Email,
                    PasswordHash = signUpViewModel.PassWord,
                    Nickname = signUpViewModel.Nickname
                };
                var result = await userManager.CreateAsync(userModel, signUpViewModel.PassWord);
                if (result.Succeeded)
                {
                    //跳转回登陆之前的页面,并登陆 生成cookie信息
                    await signInManager.SignInAsync(userModel, new AuthenticationProperties { IsPersistent = true });
                    return RedirectToUrl(returnUrl);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bolg.Web.Models.ViewModels;
using Bolg.Web.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bolg.Web.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            //roleManager.CreateAsync(new IdentityRole("admin"));       
            //var role = roleManager.FindByNameAsync("admin");
            //roleManager.AddClaimAsync(role.Result, new Claim("admin","true"));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
              
                var model = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,                   
                    Email = registerViewModel.Email

                };
                var result= await userManager.CreateAsync(model, registerViewModel.PassWord);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {   
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
               
            }
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(logInViewModel.Email);
                if (user != null)
                {   
                    
                    var result = await signInManager.PasswordSignInAsync(user, logInViewModel.PassWord, true, true);
                    if (result.Succeeded)
                    {
                       await userManager.AddClaimAsync(user, new Claim("admin", "true"));                     
                        await signInManager.SignInAsync(user, isPersistent: false);
                       
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "账号密码错误");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "账号不存在");
                }            
            }
            return View();
        }
        public async Task< IActionResult> LogOut()
        {
            await  signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task< IActionResult> AccessDenied()
        {
            var user= await userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
              var users=await userManager.GetClaimsAsync(user);
                if (users == null)
                {
                    ViewData["claims"] = "没有任何的权限";
                }
                else
                {
                    ViewData["claims"] = users[0].Value == "" ? "没有任何的权限" : users[0].Value;
                }
              
            }
            return View();
        }
    }
}
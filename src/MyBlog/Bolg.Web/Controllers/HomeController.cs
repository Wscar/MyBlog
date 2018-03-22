﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bolg.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bolg.Web.Controllers
{  
    [Authorize(Policy  ="admin")]
    public class HomeController : Controller
    {   [Authorize(Roles  ="admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
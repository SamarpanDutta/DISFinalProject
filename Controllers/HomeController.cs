using DISFinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DISFinalProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Model()
        {
            return View();
        }

        public IActionResult Aboutus()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

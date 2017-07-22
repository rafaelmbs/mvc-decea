using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_decea.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult Charts()
        {
            ViewData["Message"] = "Your chart page.";

            return View();
        }

        public IActionResult Notam()
        {
            ViewData["Message"] = "Your notam page.";

            return View();
        } 

        public IActionResult Weather()
        {
            ViewData["Message"] = "Your weather page.";

            return View();
        } 

        public IActionResult Error()
        {
            return View();
        }
    }
}

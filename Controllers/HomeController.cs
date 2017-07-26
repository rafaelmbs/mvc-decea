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
        
        public IActionResult Charts(string icao)
        {
            ViewData["Message"] = "Your chart page.";
            ViewData["icao"] = icao;

            return View();
        }

        public IActionResult Notam(string icao)
        {
            ViewData["Message"] = "Your notam page.";
            ViewData["icao"] = icao;

            return View();
        } 

        public IActionResult Weather(string icao)
        {            
            ViewData["Message"] = "Your weather page.";
            ViewData["icao"] = icao;

            return View();
        } 

        public IActionResult Error()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using mvc_decea.Services;

namespace mvc_decea.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ChartsService _service;

        public ChartsController(ChartsService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Charts(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await _service.GetCharts(icao);
                    
                    return Json(new { Cartas = result });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }
}
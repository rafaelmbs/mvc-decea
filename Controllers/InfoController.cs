using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using mvc_decea.Services;

namespace mvc_decea.Controllers
{
    public class InfoController : Controller
    {
        private readonly AeroService _service;

        public InfoController(AeroService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Info(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await _service.GetInfo(icao);

                    return Json(new { info = result });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }    
}
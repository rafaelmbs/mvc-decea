using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using mvc_decea.Services;

namespace mvc_decea.Controllers
{
    public class NotamController : Controller
    {
        private readonly NotamService _service;

        public NotamController(NotamService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Notam(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await _service.GetNotam(icao);
                    return Json(new { Notam = result });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }            
        }
    }
}
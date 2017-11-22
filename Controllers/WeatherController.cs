using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using mvc_decea.Services;

namespace mvc_decea.Controllers
{
    public class WeatherController : Controller
    {
        private readonly AeroService _aeroService;
        private readonly WeatherService _service;

        public WeatherController(AeroService aeroService, WeatherService service)
        {
            _aeroService = aeroService;
            _service = service;
        }

        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Weather(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //var aero = await _aeroService.GetInfo(icao);
                    var result = await _service.GetWeather(icao);
                    // return Json(new { Met = result, Info = aero });
                    return Json(new { Met = result });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }
}
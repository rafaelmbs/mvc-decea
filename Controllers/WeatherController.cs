using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_decea.Controllers
{
    public class WeatherController : Controller
    {
        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Weather(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://54.233.79.161:3000");
                    var response = await client.GetAsync($"/met/{icao}");
                    response.EnsureSuccessStatusCode();
    
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawChart = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);
                    return Json(new {
                        Met = rawChart.aisweb.met
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }

    public class Met
    {
        public IList<string> loc { get; set; }
        public IList<string> metar { get; set; }
        public IList<string> taf { get; set; }
    }

    public class AiswebWeather
    {
        public IList<Met> met { get; set; }
    }

    public class WeatherResponse
    {
        public AiswebWeather aisweb { get; set; }
    }
}
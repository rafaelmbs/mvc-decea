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
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> City(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=a96dc67f1d6ad570e96f9056f911faa6&units=metric");
                    response.EnsureSuccessStatusCode();
    
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return Ok(new {
                        Pressure = rawWeather.Main.Pressure,
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x=>x.Main)),
                        City = rawWeather.Name
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
    }

    public class OpenWeatherResponse
    {
        public string Name { get; set; }
    
        public IEnumerable<WeatherDescription> Weather { get; set; }
    
        public Main Main { get; set; }
    }
 
    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
 
    public class Main
    {
        public string Temp { get; set; }

        public string Pressure { get; set; }
    }
}
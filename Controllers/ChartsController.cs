using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_decea.Controllers
{
    public class ChartsController : Controller
    {
        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Charts(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://54.233.79.161:3000");
                    var response = await client.GetAsync($"/cartas/{icao}");
                    response.EnsureSuccessStatusCode();
    
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawChart = JsonConvert.DeserializeObject<CartasResponse>(stringResult);
                    return Json(new {
                        Cartas = rawChart.aisweb.cartas
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }

    public class ItemCarta
    {
        public IList<string> especie { get; set; }
        public IList<string> tipo { get; set; }
        public IList<string> tipo_descr { get; set; }
        public IList<string> nome { get; set; }
        public IList<string> IcaoCode { get; set; }
        public IList<string> dt { get; set; }
        public IList<string> link { get; set; }
        public IList<string> tabcode { get; set; }
    }

    public class Carta
    {
        public IList<ItemCarta> item { get; set; }
    }
    public class AiswebCharts
    {
        public IList<Carta> cartas { get; set; }
    }

    public class CartasResponse
    {
        public AiswebCharts aisweb { get; set; }
    }
}
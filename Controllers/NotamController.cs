using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_decea.Controllers
{
    public class NotamController : Controller
    {
        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Notam(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://54.233.79.161:3000");
                    var response = await client.GetAsync($"/notam/{icao}");
                    response.EnsureSuccessStatusCode();
    
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawChart = JsonConvert.DeserializeObject<NotamResponse>(stringResult);
                    return Json(new {
                        Notam = rawChart.aisweb.notam[0].item[0]
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }

    public class ItemNotam
    {
        public IList<string> q { get; set; }
        public IList<string> cod { get; set; }
        public IList<string> status { get; set; }
        public IList<string> tp { get; set; }
        public IList<string> dt { get; set; }
        public IList<string> n { get; set; }
        public IList<string> refe { get; set; }
        public IList<string> fir { get; set; }
        public IList<string> loc { get; set; }
        public IList<string> b { get; set; }
        public IList<string> c { get; set; }
        public IList<string> d { get; set; }
        public IList<string> e { get; set; }
        public IList<string> nof { get; set; }
        public IList<string> s { get; set; }
        public IList<string> geo { get; set; }
        public IList<string> aero { get; set; }
        public IList<string> cidade { get; set; }
        public IList<string> uf { get; set; }
        public IList<string> f { get; set; }
        public IList<string> g { get; set; }
    }

    public class Notam
    {
        public IList<ItemNotam> item { get; set; }
    }

    public class AiswebNotam
    {
        public IList<Notam> notam { get; set; }
    }

    public class NotamResponse
    {
        public AiswebNotam aisweb { get; set; }
    }
}
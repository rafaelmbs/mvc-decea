using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_decea.Controllers
{
    public class InfoController : Controller
    {
        [HttpGet("[action]/{icao}")]
        public async Task<IActionResult> Info(string icao)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://54.233.79.161:3000");
                    var response = await client.GetAsync($"/aero/{icao}");
                    response.EnsureSuccessStatusCode();
    
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawChart = JsonConvert.DeserializeObject<AeroInfoResponse>(stringResult);
                    return Json(new {
                        info = rawChart.aisweb.aero
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }

    public class Rotaer
    {
        public IList<string> alt_m { get; set; }
        public IList<string> alt_p { get; set; }
        public IList<string> caract { get; set; }
        public IList<string> pista { get; set; }
        public IList<string> cmb { get; set; }
        public IList<string> rffs { get; set; }
        public IList<string> met { get; set; }
        public IList<string> com { get; set; }
        public IList<string> rdonav { get; set; }
        public IList<string> ais { get; set; }
        public IList<string> rmk { get; set; }
        public IList<string> vfr { get; set; }
    }

    public class Aero
    {
        public IList<string> AeroCode { get; set; }
        public IList<string> name { get; set; }
        public IList<string> city { get; set; }
        public IList<string> uf { get; set; }
        public IList<string> lat { get; set; }
        public IList<string> lng { get; set; }
        public IList<string> fir { get; set; }
        public IList<Rotaer> rotaer { get; set; }
    }

    public class AiswebInfo
    {
        public IList<Aero> aero { get; set; }
    }

    public class AeroInfoResponse
    {
        public AiswebInfo aisweb { get; set; }
    }

}
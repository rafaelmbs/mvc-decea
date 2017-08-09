using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using mvc_decea.Contracts.Aero;
using mvc_decea.Repository.Views.Aero;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class AeroRepository : IAeroRepository
    {
        public async Task<IList<AeroItemView>> GetInfo(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://54.233.79.161:3000");
                var response = await client.GetAsync($"/aero/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<AeroGetResponse>(stringResult);
                return rawChart.aisweb.aero;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using mvc_decea.Contracts.Aero;
using mvc_decea.Contracts.Charts;
using mvc_decea.Repository.Views.Charts;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class ChartsRepository : IChartsRepository
    {
        public async Task<IList<ChartsView>> GetCharts(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://54.233.79.161:3000");
                var response = await client.GetAsync($"/cartas/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<ChartsGetResponse>(stringResult);
                return rawChart.aisweb.cartas;
            }
        }
    }
}
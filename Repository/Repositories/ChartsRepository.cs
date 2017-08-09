using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using mvc_decea.Contracts.Aero;
using mvc_decea.Contracts.Charts;
using mvc_decea.Repository.Views.Charts;
using mvc_decea.Settings;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class ChartsRepository : IChartsRepository
    {
        private readonly IOptions<AppSettings> _config;

        public ChartsRepository(IOptions<AppSettings> config)
        {
            _config = config;
        }

        public async Task<IList<ChartsView>> GetCharts(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.Value.ConnectionURI_ApiDecea);
                var response = await client.GetAsync($"/cartas/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<ChartsGetResponse>(stringResult);
                return rawChart.aisweb.cartas;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using mvc_decea.Contracts.Notam;
using mvc_decea.Repository.Views.Notam;
using mvc_decea.Settings;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class NotamRepository : INotamRepository
    {
        private readonly IOptions<AppSettings> _config;

        public NotamRepository(IOptions<AppSettings> config)
        {
            _config = config;
        }

        public async Task<IList<NotamItemView>> GetNotam(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.Value.ConnectionURI_ApiDecea);
                var response = await client.GetAsync($"/notam/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<NotamGetResponse>(stringResult);
                return rawChart.aisweb.notam[0].item;
            }
        }
    }
}